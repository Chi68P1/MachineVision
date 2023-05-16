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
uint8_t buf[2];
int mpu;

void sendData(uint8_t address, uint8_t data){
    buf[0] = address;
    buf[1] = data;
    wiringPiSPIDataRW(spi0, buf, 2);
}

void Init_MAX7219(void) // SPI
{
    // set decode mode: 0x09EE
    sendData(0x09,0x00); 
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

void shift_data(uint8_t data, uint8_t direction){

    sendData(1,0b00011101); // o
    sendData(2,0b00001110); // L
    sendData(3,0b00001110); // L
    sendData(4,0b01001111); // E
    sendData(5,0b00110111); // H

    sendData(2,0b00011101); // o
    sendData(3,0b00001110); // L
    sendData(4,0b00001110); // L
    sendData(5,0b01001111); // E
    sendData(6,0b00110111); // H

    sendData(3,0b00011101); // o
    sendData(4,0b00001110); // L
    sendData(5,0b00001110); // L
    sendData(6,0b01001111); // E
    sendData(7,0b00110111); // H

    sendData(4,0b00011101); // o
    sendData(5,0b00001110); // L
    sendData(6,0b00001110); // L
    sendData(7,0b01001111); // E
    sendData(8,0b00110111); // H

    sendData(1,0b00011101); // o
    sendData(2,0b00001110); // L
    sendData(3,0b00001110); // L
    sendData(4,0b01001111); // E
    sendData(5,0b00110111); // H

    if(direction == 0)
    {
        sendData(data, 0x00);
    }
    else if(direction == 1)
    {
        sendData(data, 0x40);
    }
    else if(direction == 2)
    {
        sendData(data, 0x80);
    }
    else if(direction == 3)
    {
        sendData(data, 0xC0);
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
    }
    return 0;
}