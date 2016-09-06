# Cubic Assault

After countless of hours of preparation – artillery storage, quantum research, and planning trough encoded messages, time has finally come for a war with the Spherical Race. Cubic is on the front lines devastating the enemy forces. Someone, however, must give statistics to Cubic about the count of enemies on each front. You are best for this job.

You will be given as input lines containing, a region, the type of the soldiers at that region and their amount. You must <strong>store statistics</strong> about the <strong>amount of meteors</strong> Cubic needs to defeat in <strong>every region</strong>. Note that if at one region 1 000 000 Green Meteors gather, they <strong>combine</strong> into 1 Red Meteor. By the same logic, 1 000 000 Red Meteors get <strong>combined</strong> into 1 Black Meteor. Note also, that you might receive <strong>several input lines</strong> with information about <strong>1 region</strong>. In that case just <strong>update that region’s statistics</strong>. Multiple values to one type <strong>increase</strong> that type’s value each time.

The input data will come in the following format <strong>{regionName} -> {meteorType} -> {count}.</strong>

When you receive the command <strong>“Count em all”</strong>, you must <strong>end</strong> the input sequence. You must print all the regions ordered by the <strong>amount of Black Meteors</strong> – descending, then by the <strong>length of their names</strong> – ascending, and lastly <strong>alphabetically</strong>. For every region you must print how many green, red and black meteors there. Order the printing of the types by <strong>amount of defeated units</strong> in them – descending, and if two are with the same value, order them <strong>alphabetically</strong>.

<strong>Input</strong>
<ul>
<li>As input you will receive random amount of input lines containing information about Cubic’s statistics.</li>
<li>The input ends when you receive the command “Count em all”.</li>
</ul>

<strong>Output</strong>
<ul>
<li>The output is simple. You must print each region and the statistics about the 3 types of meteors in it.</li>
<li>The format of output is :<br />
<strong>{regionName}</strong><br />
<strong>-> {firstType} : {firstTypeCount}</strong><br />
<strong>-> {secondType} : {secondTypeCount}</strong><br />
<strong>-> {thirdType} : {thirdTypeCount}</strong>
</li>
<li>The order of each type depends on its count as specified above. All data must be ordered correctly.</li>
</ul>

<strong>Constrains</strong>
<ul>
<li>The input numbers will be valid integers in the range [-2<sup>31</sup> + 1, 2<sup>31</sup> – 1].</li>
<li>The input will always be in the format specified above. There is no need to check it explicitly.</li>
<li>Allowed time/memory: 100ms/16MB</li>
</ul>

<strong>Examples</strong>
<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>Cubica -> Black -> 1<br />Cubica -> Red -> 1000<br />Spherica -> Green -> 1000000</td>
<td>Cubica<br />-> Red : 1000<br />-> Black : 1<br />-> Green : 0<br />Spherica<br />-> Red : 1<br />-> Black : 0<br />-> Green : 0<br /></td>
</tr>
</table>

<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>Triangula Canyon -> Black -> 100<br />Diagonalica -> Red -> 999999<br />Ellipsetica -> Red -> 100000000<br />Diagonalica -> Black -> 99<br />Diagonalica -> Green -> 1000000<br />Count em all</td>
<td>Diagonalica<br />-> Black : 100<br />-> Green : 0<br />-> Red : 0<br />Ellipsetica<br />-> Black : 100<br />-> Green : 0<br />-> Red : 0<br />Triangula Canyon<br />-> Black : 100<br />-> Green : 0<br />-> Red : 0<br /></td>
</tr>
</table>
