#include <wiringPiSPI.h>
#include <wiringPi.h>
#include <stdio.h>
#include <unistd.h>
#include <time.h>  // time_t, struct tm, time, localtime 
#include <stdint.h>
#include <math.h>
#include <wiringPiI2C.h>

#define Sample_rate         25
#define Config              26
#define Gygro_config        27
#define Acc_config          28
#define Interrupt           56
#define PWR_Managment       107
#define Acc_X               59
#define Acc_Y               61
#define Acc_Z               63
#define TemptHigh           65
#define Gygro_X             67
#define Gygro_Y             69
#define Gygro_Z             71  
#define spi0   0

uint8_t buf[2];
int mpu;
uint8_t delta_t;

int Read_sesor(unsigned char Tempt_register){
    int16_t MSB, LSB, data;
    MSB = wiringPiI2CReadReg8(mpu, Tempt_register);
    LSB = wiringPiI2CReadReg8(mpu, Tempt_register + 1 );
    data = (MSB<<8) | LSB;
    return data;
}

void sendData(uint8_t address, uint8_t data){
    buf[0] = address;
    buf[1] = data;
    wiringPiSPIDataRW(spi0, buf, 2);
}

void Init(void){
    // set decode mode: 0x09
    sendData(0x09,0xEE);
    // set intensity: 0x0A09
    sendData(0x0A, 9);
    // scan limit: 0x0B07
    sendData(0x0B, 7);
    // no shutdown, turn off display test
    sendData(0x0C, 1);
    sendData(0x0F, 0);
}

void Init_MPU6050(void){
    //resigter 25 -> 28 , 55, 56 ,107
    //sample rate 100hz
    wiringPiI2CWriteReg8(mpu, Sample_rate, 0x09);
    // no ext clock, DLFP 94hz
    wiringPiI2CWriteReg8(mpu, Acc_config, 0x02);
    // gyro FS: +- 500 o/s
    wiringPiI2CWriteReg8(mpu, Gygro_config, 0x08);
    // acc FS: +-8g
    wiringPiI2CWriteReg8(mpu, Acc_config, 0x10);
    // ennable interrupt Data ready
    wiringPiI2CWriteReg8(mpu, Interrupt, 0x01);
    // chọn nguồn xung Gyro X
    wiringPiI2CWriteReg8(mpu, PWR_Managment,0x01);
}

void Read_CPU(){
    FILE *tempfile;
    float temp;
    tempfile = fopen("/sys/class/thermal/thermal_zone0/temp","r");
    fscanf(tempfile, "%f",&temp);
    temp = temp / 1000;
    fclose(tempfile);

    time_t rawtime;
    struct tm *ct;

    time (&rawtime);
    ct = localtime (&rawtime);
    // dd/mm/yyyy hh:mm:ss temp
    printf ("\r %02d/%02d/%04d %02d:%02d:%02d %.2f", ct->tm_mday,ct->tm_mon+1,ct->tm_year+1900,ct->tm_hour,ct->tm_min,ct->tm_sec, temp);
    fflush(stdout);
    sleep(1);
}

struct SensorData {
    float temp;
    float Ax, Ay, Az;
    float angle_Pitch, angle_Roll;
    float rate_Pitch, rate_Roll, rate_Yaw;
    float AccXInertial, AccYInertial, AccZInertial;
    float Vx, Vy, Vz;
    float Altitude;
};

struct SensorData Read_MPU6050() {
    struct SensorData data = {0};

    delta_t = 0.004; //s
    // nhiệt độ cảm biến
    data.temp = (float)Read_sesor(TemptHigh)/340.0 + 36.53;

    // gia tốc theo trục
    data.Ax = (float)Read_sesor(Acc_X) / 4096.0; 
    data.Ay = (float)Read_sesor(Acc_Y) / 4096.0;
    data.Az = (float)Read_sesor(Acc_Z) / 4096.0;

    // góc nghiêng khi cảm biên không chuyển dộng
    data.angle_Pitch = -atan2(Ax, sqrt(pow(Ay, 2)+pow(Az, 2)))*180/M_PI;
    data.angle_Roll = atan2(Ay, sqrt(pow(Ax, 2)+pow(Az, 2)))*180/M_PI;

    // góc quay
    data.rate_Roll = Read_sesor(Gygro_X)/65.5;
    data.rate_Pitch = Read_sesor(Gygro_Y)/65.5;
    data.rate_Yaw = Read_sesor(Gygro_Z)/65.5;

    // gia tốc quán tính theo trục 
    data.AccZInertial = -sin(data.angle_Pitch*(M_PI/180))*data.Ax + cos(data.angle_Pitch*(M_PI/180))*sin(data.angle_Roll*(M_PI/180))*data.Ay + cos(data.angle_Pitch*(M_PI/180))*cos(data.angle_Roll*(M_PI/180))*data.Az;

    

    // chuyển sang cm/s2
    data.Ax = data.Ax*9.81*100;
    data.Ay = data.Ay*9.81*100;
    data.AccZInertial = (data.AccZInertial-1)*9.81*100;

    // vận tốc theo trục (cm/s)
    data.Vx = data.Vx + data.Ax*delta_t;
    data.Vy = data.Vy + data.Ay*delta_t;
    data.Vz = data.Vz + data.AccZInertial*delta_t;  

    // độ cao
    data.Altitude = data.Altitude +data.delta_t*data.Vz + 0.5*data.AccXInertial*data.delta_t*data.delta_t   

    //delay(delta_t*1000); // thời gian lấy mẫu

    while (millis() -  LoopTimer < delta_t*1000)
    {LoopTimer = millis();}

    return data
}


int main(void){
    // setup SPI interface
    wiringPiSPISetup(spi0,10000000);
    // setup I2C peripheral
    mpu = wiringPiI2CSetup(0x68);
    //check I2C connenction
    if(wiringPiI2CReadReg8(mpu, 0x85) != 0x68){
        printf("connect fail!");
    }
    // MPU6050 configuration
    Init_MPU6050();
     // set operational mode for max7219
    Init_MAX7219();

    while (1){

        struct SensorData data = Read_MPU6050();
        printf("Angle Pitch: %f\n", data.angle_Pitch);
        printf("AccXInertial: %f\n", data.AccXInertial);

    }
}