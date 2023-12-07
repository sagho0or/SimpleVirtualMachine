loadstring "Calculating (2 + 3) - 1"
* writestring 
loadimage "sample.jpg"
* displayimage
* loadint 2
* loadint 3
add
decr
loadint 1
* subtract
* loadint 10
* %loopTask8Start%
* bltint 2 endLoop 
* decr  
goto loopTask8Start
%endLoop%
loadstring "Count ="
writestring
writestring

