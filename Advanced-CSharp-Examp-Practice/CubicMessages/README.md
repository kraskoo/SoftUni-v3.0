# Cubic’s Messages

Cubic is a veteran soldier from The Great Cubic Army. He has even participated in the Spherical Invasion as a Sergeant First Class. As a veteran, Cubic has some personal security issues – he communicates only trough text messages and sends them in a specific encrypted way, which you must decrypt in order to understand what he is saying.

You will begin receiving lines of input, which will consist of random ASCII characters – Cubic’s encrypted lines. After each line you will receive a number – the length of the message he sent. Cubic might send false messages, in an act to confuse his “enemies”. You must capture only the messages that follow a certain format.

According to that format the <strong>valid</strong> messages:
<ul>
<li>Consist of <strong>m</strong> characters, where <strong>m</strong> is the integer entered after each encrypted line.</li>
<li>Has only digits before itself in the encrypted line</li>
<li>Consists only of English alphabet letters</li>
<li>Has no English alphabet letters after itself in the encrypted line</li>
</ul>

<strong>Any</strong> message that <strong>does not follow</strong> the, specified above, rules, is <strong>invalid</strong>, and you must <strong>ignore it</strong>.

After you find <strong>all valid</strong> messages, you need to find their <strong>verification code</strong>. Every message has its own verification code, which Cubic gives in order to verify the message. <strong>Take all the digits before the message</strong> and all the digits <strong>after the message</strong> and consider them as <strong>indexes</strong>. If they are <strong>valid existing</strong> indexes <strong>in the message, form a string</strong> with those indexes <strong>taking characters from the message</strong>. If an index is <strong>nonexistent</strong>, put a <strong>space<strong> there. The string you form up is the verification code for the current message.

<strong>Input</strong>
<ul>
<li>The input will always come in the form of 2 lines, except when it is the line terminating the input sequence.</li>
<li>The first input line will contain random ASCII characters, and the second – a number.</li>
<li>When the line <strong>“Over!”</strong> is entered, the input sequence ends.</li>
</ul>

<strong>Output</strong>
<ul>
<li>The output is simple. You must print all the valid messages you’ve found, each on a new line, and their verification codes, if they have such.</li>
<li>The format of output is <strong>“{message} == {verificationCode}”.</strong></li>
</ul>

<strong>Constrains</strong>
<ul>
<li>The input lines can consist of <strong>ANY ASCII</strong> character.</li>
<li>There will be <strong>NO</strong> such cases as an encrypted message without a number before it.</li>
<li>The number will be a valid integer in the range [0, 100].</li>
<li>Allowed time/memory: 100ms/16MB</li>
</ul>

<strong>Examples</strong>
<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>1234test4321<br />4<br />0000oooo0000<br />4<br />Over!</td>
<td>test == est  tse<br />oooo == oooooooo</td>
</tr>
</table>

<table>
<tr>
<th><strong>Input</strong></th>
<th><strong>Output</strong></th>
</tr>
<tr>
<td>1wat!<br />3<br />#23asd33<br />3<br />333asd3a<br />3<br />100dun2<br />3<br />Over!</td>
<td>wat == a<br />dun == uddn</td>
</tr>
</table>
