#Cubic’s Rube
Several years ago, while Cubic was researching a new quantum technology, to design a weapon he can use against the Spherical Race, he discovered a magical sub-dimension which stands in the cross-road of all other dimensions. The dimension was completely empty but it had a perfect cubic form and Cubic liked that a lot, so he named it after himself – The Cubic’s Rube.<br /><br />
Cubic noticed that the Rube gets frequently bombarded with particles which fill it, so he decided to research its volume to see how it reacts with particles. He asked for help from The Great Cubical Army, and, guess what? They sent you.<br /><br />
You will be given <strong>n</strong> – an integer specifying the 3 dimensions of The Rube. Only 1 integer is used for all 3 dimensions because The Rube is a perfect cube. After that you will start receiving lines containing 4 integers separated by a single space. The <strong>first three integers</strong> will represent a <strong>point</strong> in the 3D space, and <strong>the last integer</strong> will represent the particles. You must bombard that cell at that point, <strong>if there is such cell</strong>, with the <strong>given particles</strong>, adding to it – the value corresponding to the given 4<sup>th</sup> integer. Note that the properties specified above apply <strong>only</strong> for cells <strong>INSIDE</strong> The Rube. Also, there will be <strong>NO</strong> cell that is <strong>hit</strong> more than <strong>1 times</strong> by particles.<br /><br />

<strong>Input</strong>
<ul>
<li>The first line of input will hold an integer <strong>n</strong>.</li>
<li>After that you will begin receiving lines of input containing 4 integers separated by a space.</li>
<li>The input ends when you receive the command <strong>“Analyze”</strong>.</li>
</ul>
<br /><br />
<strong>Output</strong>
<ul>
<li>The output consists of two lines.</li>
<li>On the first line print the sum of all the cells in the Rube.</li>
<li>On the second line print the amount of cells which’s value has not been changed.</li>
</ul>
<br /><br />
<strong>Constrains</strong>
<ul>
<li>The dimensions of the matrix – n will be in the range [0, 25].</li>
<li>The integers from the input lines will be in the range [-2<sup>31</sup> + 1, 2<sup>31</sup> – 1].</li>
<li>Allowed time/memory: 100ms/16MB.</li>
</ul>
<br /><br />
<strong>Example</strong>
<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>4<br />2 2 2 2<br />Analyze</td><td>2<br />63</td>
</tr>
</table>

<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>5<br />3 2 3 10<br />-1 -1 -1 0<br />1 4 0 20<br />2 2 2 5<br />Analyze</td>
<td>35<br />122</td>
</tr>
</table>
