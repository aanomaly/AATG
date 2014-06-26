//Model do pierwszego podpunkty zadania.
//p1.props - w�a�ciwo�ci potrzebne do zrealizowania tego podpuktu
// s - stan
// d1 - pierwsza ko��
// d2 - druga ko��
// r - numer rundy
// b - aktualny bilans z�ot�wek

dtmc

module guessing

s : [0..28] init 0;
d1 : [1..6] init 1;
d2 : [1..6] init 1;
r : [1..10] init 1;
b : [-11..11] init 0;
[] s=0 & b >= 10 -> 1 : (s' = 28);
[] s=0 & b <= -10 -> 1 : (s' = 28);
[] s=0 & b < 10 & b > -10 -> 1/6 : (s'=1) + 1/6 : (s'=2) + 1/6 : (s'=3) + 1/6 : (s'=4) + 1/6 : (s'=5) + 1/6 : (s'=6) ;
[] s=1 -> 1 : (s'=8);
[] s=2 -> 1/6 : (s'=8) + 5/6 : (s'=9);
[] s=3 -> 1/10 : (s'=8) + 1/2 : (s'=9) + 2/5 : (s' = 10);
[] s=4 -> 5/14 : (s'=9) + 4/14 : (s'=10) + 5/14 : (s'=11);
[] s=5 -> 2/5 : (s'=10) + 1/2 : (s'=11) + 1/10 : (s'=12);
[] s=6 -> 5/6 : (s'=11) + 1/6 : (s'=12);
[] s=7 -> 1 : (s'=13) & (d1'=1);
[] s=8 -> 1 : (s'=13) & (d1'=2);
[] s=9 -> 1 : (s'=13) & (d1'=3);
[] s=10 -> 1 : (s'=13) & (d1'=4);
[] s=11 -> 1 : (s'=13) & (d1'=5);
[] s=12 -> 1 : (s'=13) & (d1'=6);

[] s=13 -> 1/6 : (s'=14) + 1/6 : (s'=15) + 1/6 : (s'=16) + 1/6 : (s'=17) + 1/6 : (s'=18) + 1/6 : (s'=19) ;
[] s=14 -> 1 : (s'=21);
[] s=15 -> 1/6 : (s'=21) + 5/6 : (s'=22);
[] s=16 -> 1/10 : (s'=21) + 1/2 : (s'=22) + 2/5 : (s' = 23);
[] s=17 -> 5/14 : (s'=22) + 4/14 : (s'=23) + 5/14 : (s'=24);
[] s=18 -> 2/5 : (s'=23) + 1/2 : (s'=24) + 1/10 : (s'=25);
[] s=19 -> 5/6 : (s'=24) + 1/6 : (s'=25);
[] s=20 -> 1 : (s'=26) & (d2'=1);
[] s=21 -> 1 : (s'=26) & (d2'=2);
[] s=22 -> 1 : (s'=26) & (d2'=3);
[] s=23 -> 1 : (s'=26) & (d2'=4);
[] s=24 -> 1 : (s'=26) & (d2'=5);
[] s=25 -> 1 : (s'=26) & (d2'=6);
[] s=26 & (d1 = d2 - 1) & b < 10 -> 1 : (s'=27) & (b'=b+2);
[] s=26 & d1 < d2 -1 & b > -10 -> 1  : (s'=27) & (b'=b-1);
[] s=26 & d2 = d1 - 1 & b > -10 -> 1  : (s'=27) & (b'=b-2);
[] s=26 & d2 < d1 -1  & b < 10 -> 1 : (s'=27) & (b'=b+1);
[] s=26 & d2 = d1 -> 1 : (s'=27);
[] s=27 & r<10 -> 1 : (s'=0) & (r'=r+1);
[] s=27 & r = 10 -> 1 : (s'=28);
[] s=28 -> 1 : (s'=28);

endmodule

//rewards "coin_gain"
//[] s=26 & d1 = d2 - 1 : 2;
//[] s=26 & d1 < d2 -1 : -1;
//[] s=26 & d2 = d1 - 1 : -2;
//[] s=26 & d2 < d1 -1 : 1;
//endrewards
