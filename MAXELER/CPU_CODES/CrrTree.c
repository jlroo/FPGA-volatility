#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <stdio.h>
#include <time.h>
#include <math.h>
#include <string.h>

#define PI 3.14159265358979323846
#define tolerance 0.00001
#define epsilon 0.0001
//#include "Maxfiles.h"
//#include <MaxSLiCInterface.h>

// initialize
static double dt,u,d,p1,p2,S0,X,check,v1;

double max(double num1, double num2);
double absolute(double num1);

static double CRR_Price(int CallPutTypeInt, double spotPrice, double kPrice,double Term,double rfRate,
                                   double implied_volatility, char ExerciseFlag[10], int ntimeSteps, double divYield){
        double crr_price;
        S0 = spotPrice;
        dt = Term /ntimeSteps;

        u = exp(implied_volatility * sqrt(dt));  /*size of up jump*/
        d = exp(-implied_volatility * sqrt(dt)); /*size of down jump*/
        p1 = (exp((rfRate - divYield) * dt) - d)/ (u - d);/* probability of up jump*/
        p2 = 1 - p1;/* probability of down jump*/
        int n = ntimeSteps;
        double Smat[n+1][n+1] ;// c program
        Smat[0][0] = S0;

        for(int i = 1; i< n+1;i=i+1){

             Smat[0][i] = Smat[0][i-1]*u;/* S matrix*/
             for(int j = 1; j<i+1 ;j++) Smat[j][i] = Smat[j-1][i-1]*d;
        }

       double Cmat[n+1][n+1] ;// c program /* calculated matrix*/
       for(int i=0;i<n+1;i++) Cmat[i][n] = max(CallPutTypeInt * (Smat[i][n] - kPrice), 0);  /*initialize last column of price*/

       for(int i = n-1; i>=0;i--)
           for(int j =0; j<i+1;j++)
           {
               double present_value = exp(-rfRate*dt)*(p1*Cmat[j][i+1]+p2*Cmat[j+1][i+1]);
               double immediate_val = CallPutTypeInt * (Smat[j][i] - kPrice);

               if(strcmp(ExerciseFlag, "Am")==0)
                   Cmat[j][i] = max(present_value, immediate_val);
               else
                   Cmat[j][i] = max(present_value, 0);
           }

       crr_price = Cmat[0][0];

       return crr_price;


    }/*CRR_Price*/

  /*Implied Volatility Approximation*/
  static double IV(int CallPutTypeInt, double spotPrice, double kPrice, double Term, double rfRate,
                            char ExerciseType[10], int ntimeSteps, double divYield, double mktP){

	    double S0 = spotPrice;
        double iv;

        X = kPrice * exp(-rfRate * Term);
        if (CallPutTypeInt ==1) {

                   check = pow(mktP-(spotPrice - X) / 2, 2) - pow((spotPrice - X), 2)/PI;
                   if (check>= 0){
                       v1= sqrt(2*PI/Term)*1/(spotPrice+X)*(mktP -(spotPrice -X)/2 + sqrt(pow(mktP
                               -(spotPrice - X)/2,2) -pow(spotPrice - X, 2)/PI));/*Corrado-Miller*/

                   }
                   else{
                       v1= sqrt(2*PI / Term)*(mktP - (spotPrice - X) / 2) / (spotPrice - (spotPrice - X) / 2);/*Bharadia-Christopher-Salkin*/

                   }

                   if(v1 <= 0) printf ("Volatility <= 0, please input new parameters \n");
               }/*if*/
        else if (CallPutTypeInt ==-1)  {

                 check =  pow(mktP - (X - spotPrice) / 2,2)- pow(X - spotPrice,2)/PI;

                 if (check>= 0)
                 {
                     v1= sqrt(2*PI/Term)*1/(spotPrice+X)*(mktP -(spotPrice -X)/2 + sqrt(pow(mktP
                             -(spotPrice - X)/2,2) -pow(spotPrice-X,2)/PI));/* Corrado-Miller*/
                 }
                 else {
                     v1 = sqrt(2 * PI / Term) * (mktP - (X - spotPrice ) / 2) / (spotPrice - (spotPrice - X) / 2);// 'Bharadia-Christopher-Salkin
                 }
                 if(v1 <= 0) printf ("Volatility <= 0, please input new parameters \n");
             }/* if else*/

        for(int counter =0; tolerance>0;counter++) {

            double value1 = CRR_Price(CallPutTypeInt, S0, kPrice, Term, rfRate, v1, ExerciseType, ntimeSteps, divYield);
            double diff = mktP - value1;
            if (absolute(diff) <= tolerance) break;

            double v2 = v1 - epsilon;
            double value2 = CRR_Price(CallPutTypeInt, S0, kPrice, Term, rfRate, v2, ExerciseType, ntimeSteps, divYield);
            double dx = (value2 - value1) / epsilon;
            v1 = v1 - (mktP - value1) / dx;

            if(v1<=0) {
               printf("Volatility <= 0, please input new parameters  \n");
               break;
            }

        } /*for*/

        iv = v1;
        return iv;
  }/*IV*/

double max(double num1, double num2)
{
   double result;

   if (num1 > num2)
      result = num1;
   else
      result = num2;

   return result;
}/*max*/

double absolute(double num1)
{
   double result;

   if (num1 <0)
      result = -num1;
   else
      result = num1;

   return result;
}/* absolute*/

int main()
{
	clock_t t1, t2;

	/* INPUT datas */
	double spotPrice = 100;/*D8 */	double rfRate = 0;//D9
	double Term = 1;/*D10*/	double kPrice = 95;//D11
    char CallPutType[10] = "Call";/*D12*/    char ExerciseFlag[10] = "Am";//D13
	int ntimeSteps = 100;/*D14*/	double divYield = 0;/*D15*/
	double mktP = 20;/*D16*/

    t1 = clock();

    int CallPutTypeInt=0;
	if(strcmp(CallPutType, "Call")==0)CallPutTypeInt =1;
	else if(strcmp(CallPutType,"Put") ==0)CallPutTypeInt =-1;

	double implied_volatility;
	double check_price;

	implied_volatility = IV(CallPutTypeInt, spotPrice, kPrice, Term, rfRate, ExerciseFlag, ntimeSteps, divYield, mktP);

	printf("Implied_volatility issss %f \n", implied_volatility );// Output D20

    if (implied_volatility > 0)
        {
            check_price = CRR_Price(CallPutTypeInt, spotPrice, kPrice, Term, rfRate, implied_volatility, ExerciseFlag, ntimeSteps, divYield);
            printf("Check_price is %f \n" ,check_price);//Output D19
        }
        else  printf("Check_price is null  \n");

    t2 = clock();
	printf("%f Ms\n", (double)(t2 - t1) / CLOCKS_PER_SEC * 1000);

	return 0;
}/*main*/

/*
Value 1  19.892951
Value 2  19.889169
Value 1  19.999982
Value 2  19.996201
Implied_volatility issss 0.449797
Check_price is 20.000000
89.000000 Ms

Process returned 0 (0x0)   execution time : 0.500 s
Press any key to continue.



*/


