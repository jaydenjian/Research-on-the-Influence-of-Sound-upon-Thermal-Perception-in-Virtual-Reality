# 虛擬實境中聲音對熱觸覺回饋影響之研究<img src="media/GameScene2.png" width=20% align="right"/>
### Research on the Influence of Sound upon Thermal Perception in Virtual Reality

<br>

## 介紹 Introduct

* 在VR遊戲中，透過多模態感知刺激(Cross-model correspondence)，藉由聲音來影響人對於熱觸覺的感知。<img src="media/裝置手部圖(外).png" width=20% align="right"/>
* 硬體裝置包含一副搭載發熱貼片、彎曲感測器及 HTC VIVE 移動定位器的手套、頭戴式顯示器與耳機。
* 可以根據受測者的手掌彎曲幅度， 調整發熱貼片之溫度及輸出音量之大小。
* 玩家要在虛擬實境遊戲中，正確地調控裝置溫度來操縱遊戲中人物手掌的噴射火焰大小，移動至終點。


<br>

硬體使用-`Arduino nano`做開發。<br><br>
軟體使用-`Unity`、`Arduino IDE`進行遊戲開發與裝置控制，最後將兩者結合。


<br>

## 目錄 Table of content
* [遊戲內容](#遊戲內容-Game-Design)
* [裝置設計](#裝置設計-Device-Design)
  * 使用元件
  * 裝置架構
  * 電路設計
  * 裝置製作
* [遊戲設計](#遊戲設計-Game-Develope)
  * 系統關係圖 System Context Diagram
  * Unity與Arduino串接
  * 遊戲功能設定
  * 遊玩流程
* [論文](#論文-Thesis)

<br>

## 遊戲內容 Game Design
此遊戲主要想藉由「***音量的漸增***」與「***溫度的提升***」所產生之跨模態效應，來強化受測者在虛擬實境中對於熱觸覺的感知。<br>
* 情境設定：玩家必須使用雙手控制手中之噴射火焰，飛至終點線 以完成任務。
* 功能設定：只有當雙手溫度一致時，玩家才會向上飛， 系統會自動判斷雙手溫度是否一致，若溫度一致之狀態維持超過3秒，則人物向上飛，過程中，雙手溫度一致之狀態只要一中斷，玩家將會立即停止向上移動。 
* [遊戲影片](https://youtu.be/bDB4SQxeE-E?t=45)：<br>
<br> <a href="https://youtu.be/bDB4SQxeE-E?t=45" target="_blank"><img src="media/GameScene1.png" 
width=70% /></a>

<br>

## 裝置設計 Device Design
* **使用元件**
  * 電熱片
    > 電熱片為 60*60mm/17歐姆之電熱片，厚度約為 0.25mm，能夠貼合於掌心且能彎曲，用於熱觸覺回饋。<br> <img src="media/電熱片.JPG" width=15% >   
  * 彎曲感測器Flex Sensor SpectraSymbol 2.2′′
    > 用於測量手掌的彎曲幅度。 <br> <img src="media/彎曲感測器.png" width=15% >
  * 關節炎壓縮手套
    > <br> <img src="media/關節炎手套.png" width=15% >
  * Arduino Nano 開發板
    > <br> <img src="media/Arduino nano.png" width=15% >
  * L298N 直流驅動電路
    > 因**Arduino Nano本身之電壓不足以供應兩片電熱片所需**，故改以L298N直流驅動電路進行電壓的分配，而Arduino Nano則進行L298N上的邏輯控制。電源輸入使用24伏特/1安培的直流變壓器進行供電。<br> <img src="media/L298N.png" width=15% >
  <br>
  
* **裝置架構**<br> <img src="media/Device architecture-03.png" width=60% >
<br>

* **電路設計**
<br>
<br> <img src="media/電路圖.png" width=70%>
<br>

* **裝置製作**
<br>
<br> <img src="media/裝置架構圖-14.png" width=70%>
<br>
<br> <img src="media/IMG_2844.JPG" width=70%>
<br>

## 遊戲設計 Game Develope
* **系統關係圖System Context Diagram**
<br>

> <img src="media/系統關係圖v3  System Context Diagram-16.png" width=70%><br>
>> 1. 透過Unity播放火聲音效。<br>
>> 2. Unity利用Serial Port方式傳遞控制訊號給Arduino。<br>
>> 3. Arduino發出Analog信號給觸覺回饋裝置。<br>
>> 4. 觸覺回饋裝置：手掌內側為發熱貼片，依據信號大小給予使用者相應的熱觸覺回饋。手背裝有VR訊號追蹤器(VIVE Tracker)，追蹤使用者手部位置。<br>
>> 5. 使用者必須配戴VR頭戴式顯示器、訊號追蹤器(VIVE Tracker)、耳機與觸覺回饋裝置。<br>
>> 6. VR 訊號追蹤器(VIVE Tracker):將位置訊息傳回 Unity。<br>

<br>

* **Unity與Arduino串接**
<br>

> **Arduino IDE** <br>
> 使用執行緒Thread的方式，將數值傳入Unity。<br>
> 先將字串相加，再輸出至Unity。
 ```C
 void loop() {

   //FlexSensor Part
   int flex_value1 = analogRead(flex_pin1);
   int flex_value2 = analogRead(flex_pin2);
   flex_value1 = map(flex_value1,0,1023,0,255);
   flex_value2 = map(flex_value2,0,1023,0,255);
   /////

   /////resize flexSensor value for Heating
   if(flex_value1<41){
     flex_value1=41;
   }else if(flex_value1>71){
     flex_value1=71;
   }
   if(flex_value2<53){
     flex_value2=53;
   }else if(flex_value2>70){
     flex_value2=70;
   }
   int heat_value1 = map(flex_value1,41,71,0,50);
   int heat_value2 = map(flex_value2,53,70,0,50);

   //Sending value to unity
   String sending_value = String(flex_value1) +","+ String(flex_value2) + "," + String(heat_value1) + "," + String(heat_value2);
   Serial.println(sending_value);
  ```
  <br>
  
 > **Unity** <br>
 > 透過[ArduinoBasic.cs](script/ArduinoBasic.cs)，先讀取Arduino數值後，再將字串轉換成浮點數，以便後續進行控制。
 ```C
 void Update()
 {
  //read Arduino message, String to Float
  arduinoMessage = player.GetComponent<ArduinoBasic>().readMessage;
  message = float.Parse(arduinoMessage);
 ```
 <br>
 
* **遊戲功能設定**<br> <img src="media/功能設定流程圖.png" width=50% >
<br>


* **遊玩流程**<br> <img src="media/遊戲流程圖.png" width=30% >
<br>

* **Game Design Class Diagram**<br> <img src="media/Deisgn Class Diagram-Ver3.png">
<br>


## 論文 Thesis
[論文連結](https://hdl.handle.net/11296/4za5z3)

