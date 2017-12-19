#define trigPin 6
#define echoPing 7

const int trig = 6;
const int echo = 7;

float sure = 0;
float mesafe = 0;

const int s0 = 8;  
const int s1 = 9;  
const int s2 = 12;  
const int s3 = 11;  
const int out = 10;   

int redLed = 2;  
int greenLed = 3;  
int whiteLed = 4;
 
int red = 0;  
int green = 0;  
int white = 0; 

int r_apple = 0;
int g_apple = 0;
int show = 0;

String gelen;

    
void setup()   
{  
  Serial.begin(9600); 
  pinMode(trig, OUTPUT);
  pinMode(echo, INPUT);
  pinMode(s0, OUTPUT);  
  pinMode(s1, OUTPUT);  
  pinMode(s2, OUTPUT);  
  pinMode(s3, OUTPUT);  
  pinMode(out, INPUT);  
  pinMode(redLed, OUTPUT);  
  pinMode(greenLed, OUTPUT);  
  pinMode(whiteLed, OUTPUT);  
  digitalWrite(s0, HIGH);  
  digitalWrite(s1, HIGH);  
}  
    
void loop() 
{     
 

  gelen = Serial.readString();
  

  
  digitalWrite(trig, HIGH);
  delayMicroseconds(1000);
  digitalWrite(trig, LOW);
  sure = pulseIn(echo, HIGH);
  mesafe = (sure/2)/29.1;

 
  
  color(); 
  
  //Serial.println();  

  if (red < white && red < green && red < 20)
  {  
   
   digitalWrite(redLed, HIGH); // Kırmızı led
   digitalWrite(greenLed, LOW);  
   digitalWrite(whiteLed, LOW);  
  }  

  else if (white < red && white < green)   
  {  
    
   digitalWrite(redLed, LOW);  
   digitalWrite(greenLed, LOW);  
   digitalWrite(whiteLed, HIGH); // beyaz led 
  }  

  else if (green < red && green < white)  
  {  
    
   digitalWrite(redLed, LOW);  
   digitalWrite(greenLed, HIGH); // yeşil led 
   digitalWrite(whiteLed, LOW);  
  }  
 
  //delay(300);   
  digitalWrite(redLed, LOW);  
  digitalWrite(greenLed, LOW);  
  digitalWrite(whiteLed, LOW);  

  
 
  if(mesafe<10.0)
  {
 
    if(show == 0)
    {
        
        if (red < white && red < green && red < 20)
        {
          r_apple = r_apple+1;
          String strAdet = String(r_apple);
          String message = "K-" + strAdet;
          Serial.println(message);
          show = 1;
          delay(1000);
          
        }
        else if (green < red && green < white)
        {
          g_apple = g_apple+1; 
          String strAdet1 = String(g_apple);
          String message = "Y-" + strAdet1;
          Serial.println(message);
          show = 1; 
          delay(1000);
        
        }
     }
  }
  else 
  {
    show =0;
  }
 } 
  
   
    
void color()  
{    
  digitalWrite(s2, LOW);  
  digitalWrite(s3, LOW);  
  //count OUT, pRed, RED  
  red = pulseIn(out, digitalRead(out) == HIGH ? LOW : HIGH);  
  digitalWrite(s3, HIGH);  
  //count OUT, pBLUE, WHITE  
  white = pulseIn(out, digitalRead(out) == HIGH ? LOW : HIGH);  
  digitalWrite(s2, HIGH);  
  //count OUT, pGreen, GREEN  
  green = pulseIn(out, digitalRead(out) == HIGH ? LOW : HIGH);  
}
