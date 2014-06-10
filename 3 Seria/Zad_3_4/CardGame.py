from pulp import *

#Ewe

prob = LpProblem("Card Ewe Problem", LpMaximize);
U = LpVariable('U', cat='Continuous');
V1 = LpVariable('V1', lowBound = 0, upBound = 1, cat='Continuous');
V2 = LpVariable('V2', lowBound = 0, upBound = 1, cat='Continuous');
V3 = LpVariable('V3', lowBound = 0, upBound = 1, cat='Continuous');
V4 = LpVariable('V4', lowBound = 0, upBound = 1, cat='Continuous');
prob += U, "";
prob += U <= -5*V1 + 5*V2 + 5*V3 - 5*V4 , "Adam V1";
prob += U <= 5*V1 - 5*V2 + 5*V3 - 5 *V4, "Adam V2";
prob += U <= 5*V1 + 5*V2 - 5*V3 - 5 *V4, "Adam V3";
prob += U <= -5*V1 -5*V2 - 5*V3 + 5*V4, "Adam V4";
prob += V1 + V2 + V3 + V4 == 1, "";


#prob.writeLP("Guessing.lp")

prob.solve()

print "Ewe Status:", LpStatus[prob.status]


guessingout = open('Ewe.txt','w')

guessingout.write("U = " + str(U.varValue) + "\n");
guessingout.write("V1 = " + str(V1.varValue) + "\n");  
guessingout.write("V2 = " + str(V2.varValue) + "\n"); 
guessingout.write("V3 = " + str(V3.varValue) + "\n"); 
guessingout.write("V4 = " + str(V4.varValue) + "\n");              
guessingout.close()



print "Ewe Solution Written to Ewe.txt"

#ADAM
prob = LpProblem("Card Adam Problem", LpMaximize);
U = LpVariable('U', cat='Continuous');
V1 = LpVariable('V1', lowBound = 0, upBound = 1, cat='Continuous');
V2 = LpVariable('V2', lowBound = 0, upBound = 1, cat='Continuous');
V3 = LpVariable('V3', lowBound = 0, upBound = 1, cat='Continuous');
V4 = LpVariable('V4', lowBound = 0, upBound = 1, cat='Continuous');
prob += U, "";
prob += U <= 5*V1 - 5*V2 - 5*V3 + 5*V4 , "Eve V1";
prob += U <= -5*V1 + 5*V2 - 5*V3 + 5 *V4, "Eve V2";
prob += U <= -5*V1 - 5*V2 + 5*V3 + 5 *V4, "Eve V3";
prob += U <= 5*V1 + 5*V2 + 5*V3 - 5*V4, "Eve V4";
prob += V1 + V2 + V3 + V4 == 1, "";


#prob.writeLP("Guessing.lp")

prob.solve()

print "Adam Status:", LpStatus[prob.status]


guessingout = open('Adam.txt','w')

guessingout.write("U = " + str(U.varValue) + "\n");
guessingout.write("V1 = " + str(V1.varValue) + "\n");  
guessingout.write("V2 = " + str(V2.varValue) + "\n"); 
guessingout.write("V3 = " + str(V3.varValue) + "\n"); 
guessingout.write("V4 = " + str(V4.varValue) + "\n");               
guessingout.close()


print "Adam Solution Written to Adam.txt"