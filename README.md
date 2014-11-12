Go-Go-Selection-Manipulation-Technique
======================================

Implementation of a novel Go-Go variant
Summary
The main logic behind Dynamic Go-Go technique is same as the simple go-go technique. However, in this case, the user would be able to dynamically change the non-linear mapping factor of the virtual hand. This enables the user to select the objects at certain distance which was otherwise not possible through the simple go-go technique.
How it works
 In the virtual world, the user can change the non- linear mapping factor by pressing plus or minus button on the Wii remote.
 If the user presses plus, then the mapping factor is increased and the user will be able to reach out to the objects far away.
 On the other hand, if the user presses minus button, then the mapping factor is reduced and the user will be able to make fine movements beyond the threshold.
Steps to follow with 5-UDE
This technique is associated with Right Wii Remote only, so that it could be differentiated with Left Wii Remote for verification purposes.
There are 4 objects in the environment, each of which could be manipulated.
1. Increase Right Wiimote Z value to a positive value. As you can see, when the value reaches beyond the threshold (0.8), then the Go-Go sets in. This is normal Go-Go.
2. Suppose you want to increase the magnitude of mapping factor, then click on the Plus button of Right Wii remote and repeat step 1.
3. Suppose you want to decrease the magnitude to make finer movements, then click on the Minus button of Right Wii remote and repeat step 1.
4. You can also grab the objects similar to virtual hand technique by pressing both A and B buttons on right wii remote.
5. After grabbing, again the grabbed objects follows the same Go-go mapping since it is attached to the virtual hand.
6. To verify the plus and minus buttons working, compare the distance with left Wii remote. This serves as the confirmation of the working of the technique.
