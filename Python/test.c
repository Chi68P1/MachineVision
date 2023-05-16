// MPU6050 and MAX7219

#include <wiringPi.h> 
#include <wiringPiI2C.h>
#include <stdio.h> // delay
#include <stdint.h> // int16_t
#include <stdlib.h>
#include <math.h> // gcc .... -lwringPi -lm
#include <wiringPiSPI.h>

#define SampleRate              25
#define Config                  26
#define Gyro_config             27 
#define Acc_config              28
#define Interrupt_config        56
#define PWR_Management          107
#define Acc_X                   59
#define Acc_Y                   61
#define Acc_Z                   63
#define spi0                    0

// global variables
int mpu;
uint8_t buf[2];
unsigned char Data[8]={0x10,0x30,0x7f,0xff,0xff,0x7f,0x30,0x10};
uint8_t mode;

void sendData(uint8_t address, uint8_t data){
    buf[0] = address;
    buf[1] = data;
    wiringPiSPIDataRW(spi0, buf, 2);
}

void Init_MAX7219(void) // SPI
{
    // set decode mode: 0x09EE
    sendData(0x09,0xEE); // 11101110 - 1 code B decoded - 0 on segment
    // set intensity: 0x0A09
    sendData(0x0A, 9);
    // scan limit: 0x0B07
    sendData(0x0B, 7);
    // no shutdown, turn off display test
    sendData(0x0C, 1);
    sendData(0x0F, 0);
}

void Init_MPU6050(void) //I2C
{
    // register 25->28,56,107
    // sample rate 500Hz = 8000/(15+1)
    wiringPiI2CWriteReg8(mpu, SampleRate, 15); 
    // Không sử dụng nguồn xung ngoài, tắt DLFP
    wiringPiI2CWriteReg8(mpu, Config, 0);
    // gyro FS : +- 500 o/s
    wiringPiI2CWriteReg8(mpu, Gyro_config, 0x08);
    // acc FS : +- 8g
    wiringPiI2CWriteReg8(mpu, Acc_config, 0x10);
    // mở interrupt của data ready
    wiringPiI2CWriteReg8(mpu, Interrupt_config, 1);
    // chọn xung nguồn Gyro X
    wiringPiI2CWriteReg8(mpu, PWR_Management, 0x01);
}

int16_t read_sensor(unsigned char sensor)
{
    int16_t high, low, data;
    high = wiringPiI2CReadReg8(mpu, sensor);
    low = wiringPiI2CReadReg8(mpu, sensor+1);
    data = (high << 8) | low; // 16bits 
    return data;
}

void display_float(float num, uint8_t dec, uint8_t pos){
    int32_t integerPart = num;
    int32_t fractionalPart = (num - integerPart) * pow(10,dec);
    int32_t number = integerPart*pow(10,dec) + fractionalPart;
    // count the number of digits
    uint8_t count=1;
    int32_t n = number;
    while(n/10){
        count++;
        n = n/10;
    }
    
    // sendData(0x0B, count-1);
    if (count ==3) {
        for(int i=0; i<count;i++){
        if(i==dec)
            sendData(i+pos+2,(number%10)|0x80); // turn on dot segment 
        else
            sendData(i+pos+2,number%10);
        number = number/10;
    }
    }
    else if(count ==2) {
        for(int j=0; j<count;j++){
        if(i==dec)
            sendData(j+pos+1,(number%10)|0x80); // turn on dot segment 
        else
            sendData(j+pos+1,number%10);
        number = number/10;
        sendData(4+pos, 0x0F);
    }
    }
    // dislay độ
    sendData(pos+1,0x63);
}

void display_blank(uint8_t mode){
    if (mode ==1){
        for(int i=5;i<8;i++){
            sendData(i+1, 0x0F);
        }
        sendData(5, 0x00);
    }
        
    if (mode ==2){
        for(int i=1;i<4;i++){
            sendData(i+1, 0x0F);
        }
        sendData(1, 0x00);
    }

    if (mode ==3){
        for(int i=5;i<8;i++){
            sendData(i+1, 0x0F);
        }
        for(int i=1;i<4;i++){
            sendData(i+1, 0x0F);
        }
        sendData(1, 0x00);
        sendData(5, 0x00);
    }
        
}

int main()
{
    // setup ưiringPi
    wiringPiSetupPhys(); 
    // setup I2C peripheral
    mpu = wiringPiI2CSetup(0x68);
    // setup SPI interface
    wiringPiSPISetup(spi0, 10000000);
    // check connection
	if(wiringPiI2CReadReg8(mpu, 0x75)!= 0x68){
		printf("Connection fail. \n");
		exit(1);
	}   
    // GPIO configuration
    pinMode(LED, OUTPUT);
    // MPU6050 configuration
    Init_MPU6050();
     // set operational mode for max7219
    Init_MAX7219();

    while (1)
    {
        // read values from MPU6050
        float Ax = (float)read_sensor(Acc_X)/4096.0; // gia tốc theo trục X
        float Ay = (float)read_sensor(Acc_Y)/4096.0;
        float Az = (float)read_sensor(Acc_Z)/4096.0;

        float pitch = fabs(atan2(Ax,sqrt(pow(Ay,2)+pow(Az,2)))*180/M_PI); // M_PI = 180 degrees <math.h>
        float roll = fabs(atan2(Ay,sqrt(pow(Ax,2)+pow(Az,2)))*180/M_PI); 

        printf("Pitch: %f\n", pitch); // Góc nghiêng
        printf("Roll: %f\n", roll);

        if ((pitch>30.0) && (roll>30.0))
        {
           mode = 3;
        }  
        else if (pitch>30.0)
        {
            mode = 2;
        }   
        else if (roll>30.0)
        {
            mode = 1;
        }   
        else mode = 0;

        if (mode != 0)
        {
            display_blank(mode);
            delay(200);
            display_float(roll,1,4);
            display_float(pitch,1,0);
            delay(200);
        }
        else {
            display_float(roll,1,4);
            display_float(pitch,1,0);
        }

        delay(200); // update time
    }
    return 0;
}