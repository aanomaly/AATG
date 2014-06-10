from pulp import *


prob = LpProblem("Guessing Game", LpMaximize);
U = LpVariable('U', cat='Continuous');
V1 = LpVariable('V1', lowBound = 0, upBound = 1, cat='Continuous');
V2 = LpVariable('V2', lowBound = 0, upBound = 1, cat='Continuous');
V3 = LpVariable('V3', lowBound = 0, upBound = 1, cat='Continuous');
V4 = LpVariable('V4', lowBound = 0, upBound = 1, cat='Continuous');
V5 = LpVariable('V5', lowBound = 0, upBound = 1, cat='Continuous');
V6 = LpVariable('V6', lowBound = 0, upBound = 1, cat='Continuous');
prob += U, "";
prob += U <= -2*V2 + V3 + V4 + V5 + V6, "P V1";
prob += U <= 2*V1 - 2*V3 + V4 + V5 + V6, "P V2";
prob += U <= -V1 + 2*V2 - 2*V4 + V5 + V6, "P V3";
prob += U <= -V1 - V2 + 2*V3 - 2*V5 + V6, "P V4";
prob += U <= -V1 - V2 - V3 + 2*V4 - 2*V6, "P V5";
prob += U <= -V1 - V2 - V3 - V4 + 2*V5, "P V6";
prob += V1 + V2 + V3 + V4 + V5 + V6 == 1, "";


prob.writeLP("Guessing.lp")

prob.solve()

print "Status:", LpStatus[prob.status]


guessingout = open('Guessing.txt','w')

guessingout.write("U = " + str(U.varValue) + "\n");
guessingout.write("V1 = " + str(V1.varValue) + "\n");  
guessingout.write("V2 = " + str(V2.varValue) + "\n"); 
guessingout.write("V3 = " + str(V3.varValue) + "\n"); 
guessingout.write("V4 = " + str(V4.varValue) + "\n"); 
guessingout.write("V5 = " + str(V5.varValue) + "\n"); 
guessingout.write("V6 = " + str(V6.varValue) + "\n");               
guessingout.close()


print "Solution Written to Guessing.txt"
