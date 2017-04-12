#include "PatternPlayer.h"

//Output variables
int EDA = 0;              // Raw EDA measurement
int IBI = 600;            // Time interval between heart beats
int rawPulseSensor = 0;   // Raw reading from the pulse sensor pin (can be used for plotting the pulse)
int FSRValue = 0;
int FSRValue2 = 0;
int FSRValue3 = 0;
int FSRValue4 = 0;



//Sensor Variables
int EDAPin = A0;
int PulsePin = A1;         // Pulse Sensor purple wire connected to analog pin 0
int FRSPin =A2;
int FRSPin2 =A3;
int FRSPin3 =A4;
int FRSPin4 =A5;
int IBISign = 1;           // used to change the sign of the IBI, to indicate a new value (in case two succeeding values are the same)
boolean Pulse = false;     // "True" when a heartbeat is detected. (can be used for for blinking an LED everytime a heartbeat is detected) 
bool NewPulse = false;

bool firstPattern = false;
bool secondPattern = false;

PatternPlayer myLED0(3);
PatternPlayer myLED1(5);

//Serial variables
bool Connected = false;
const unsigned int serialOutputInterval = 10; // Output Frequency = 1000 / serialOutputInterval = 1000 / 10 = 100Hz
unsigned long serialLastOutput = 0;
const char StartFlag = '#';
const String Delimiter = "\t";

const Pattern twoBlinks(
                    8,
                    (byte[])          {255, 0, 100, 0, 255, 0, 100, 0},
                    (unsigned int[])  {400, 400, 400, 400, 400, 400, 400, 400}
                    );

//Example of pattern that blinks three times, with decaying intensity and then waits for 800 ms
Pattern threeBlinks(
                       8,
                    (byte[])          {100, 0, 255, 0, 100, 0, 255, 0},
                    (unsigned int[])  {200, 200, 200, 200, 200, 200, 200, 200}
                    );

Pattern PatternList[] = {twoBlinks, threeBlinks};
const int PatternListSize = sizeof(PatternList)/sizeof(Pattern);

//Also possibleto create player lists (their loop() need to be called seperately, as the array copies the objects on initialization)
PatternPlayer PlayerList[] = {myLED0, myLED1};

void setup() {
  Serial.begin(115200);
  pinMode(7, INPUT_PULLUP);
  
//Example input/output pins
  pinMode(13, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(9, OUTPUT);
  
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT);  //FL
  pinMode(A3, INPUT);  //FR
  pinMode(A4, INPUT);  //RL
  pinMode(A5, INPUT);  //RR
  pinMode(3, OUTPUT);
  pinMode(5, OUTPUT);

  analogReference(EXTERNAL);
}

int index = 0;
void loop() {
  myLED0.loop();
  myLED1.loop();
  ReadSensors(); //Have the Arduino read it's sensors etc.

  SerialInput(); //Check if Unity has send anything

  SerialOutput(); //Check if it is time to send data to Unity

  digitalWrite(13,Pulse); // Light up the LED on Pin 13 when a pulse is detected

  if(firstPattern && !myLED0.playing){
      myLED0.playPattern(twoBlinks, true); //the 'true' indicates that the pattern should fade between intensities (requires a PWM pin)
      firstPattern = false;
  }
    
    //patternPlayer.playPattern(Pattern pattern, bool fade, int repeat). fade and repeat are optinal. repeat specifices how many times the pattern should repeat after the first 

    //it is also possible to only activate a pattern if none is currently playing (not fading + repeating twice):
    if(secondPattern && !myLED1.playing){
      myLED1.playPattern(threeBlinks, false, 2); 
      secondPattern = false;
    }

    if(index == PatternListSize)
      index = 0;
}

void ReadSensors()
{
  EDA = analogRead(EDAPin); //Read the raw 

FSRValue = analogRead(FRSPin);
FSRValue2= analogRead(FRSPin2);
FSRValue3= analogRead(FRSPin3);
FSRValue4= analogRead(FRSPin4);


  checkPulseSensor();
  
}

const int inputCount = 7; //This must match the amount of bytes you send from Unity!
byte inputBuffer[inputCount];
void SerialInput()
{  
  if(Serial.available() > 0){ //check if there is some data from Unity
     Serial.readBytes(inputBuffer, inputCount); //read the data
     //Use the data for something
     if(inputBuffer[0]==1) firstPattern = true;
     if(inputBuffer[1]==1) secondPattern = true;
           

           
     //digitalWrite(3, inputBuffer[0]);
     //digitalWrite(5, inputBuffer[1]);
     digitalWrite(10, inputBuffer[2]);
     digitalWrite(9, inputBuffer[3]);

     //You could for example use the data for playing patterns
     //e.g. first value indicates which player and second value indicates which pattern to play
     }

     //Currently no checks for desync (no start/end flags or package size checks)
     //This should be implemented to make the imp. more robust
}



void SerialOutput() {
  //Time to output new data?
  if(millis() - serialLastOutput < serialOutputInterval)
    return;
  serialLastOutput = millis();


  //Write data package to Unity
  Serial.write(StartFlag);    //Flag to indicate start of data package
  Serial.print(millis());     //Write the current "time"
  Serial.print(Delimiter);    //Delimiter used to split values
  Serial.print(EDA);          //Write a value
  Serial.print(Delimiter);    //Write delimiter

  if(NewPulse){
    Serial.print(IBI);        //Only print IBI if a new pulse has been detected
    NewPulse = false;
  } else {
    Serial.print(0);          //else print 0
  }
  
  Serial.print(Delimiter);
  Serial.print(rawPulseSensor);
  Serial.print(Delimiter);
  Serial.print(FSRValue);
  Serial.print(Delimiter);
  Serial.print(FSRValue2);
  Serial.print(Delimiter);  
  Serial.print(FSRValue3);
  Serial.print(Delimiter);  
  Serial.print(FSRValue4);
  Serial.print(Delimiter);
  Serial.println();           // Write endflag '\n' to indicate end of package
 // Serial.println(analogRead(PulsePin));

  //For debugging. Comment the lines above and uncomment one of these.
  //Serial.println(analogRead(EDAPin));
  //Serial.println(analogRead(PulsePin));
}



