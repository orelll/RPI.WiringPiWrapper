using System;

namespace RPI.WiringPiWrapper
{
    public class wiringPiNodeStruct
    {
        public int pinBase;
        public int pinMax;

        public int fd; // Node specific
        public int data0; //  ditto
        public int data1; //  ditto
        public int data2; //  ditto
        public int data3; //  ditto

        public Action pinMode;
        public Action pullUpDnControl;
        public Action digitalRead;
        public Action digitalWrite;
        public Action pwmWrite;
        public Action analogRead;
        public Action analogWrite;

        public wiringPiNodeStruct(int pinBase, int pinAll)
        {
            this.pinBase = pinBase;
            this.pinMax = pinAll;
        }

        //void (* PinMode) (private struct wiringPiNodeStruct *node, private int pin, private int mode) ;

        //void (* PullUpDnControl) (private struct wiringPiNodeStruct *node, private int pin, private int mode) ;

        //int (* DigitalRead) (private struct wiringPiNodeStruct *node, private int pin) ;

        //void (* DigitalWrite) (private struct wiringPiNodeStruct *node, private int pin, private int value) ;

        //void (* PWMWrite) (private struct wiringPiNodeStruct *node, private int pin, private int value) ;

        //int (* analogRead) (private struct wiringPiNodeStruct *node, private int pin) ;

        //void (* analogWrite) (private struct wiringPiNodeStruct *node, private int pin, private int value) ;

        //private struct wiringPiNodeStruct *next ;
    }
}