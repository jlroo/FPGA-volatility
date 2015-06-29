

/***************** CME PROJECT *******************
 *
 * Implied Volatility for Options
 * on Futures Using the Cox-Ross-Rubinstein Model
 *
 * This Program use the Bharadia, Christopher,
 * and Salkin Model to calcula implied volatility.
 *
 ****************** NOTATION *********************
 *
 * T    = Term one month = 1/12
 * pi   = 3.1415927
 * S    = Underlying
 * C    = Call Option Price
 * k    = Strike Price
 * r    = Risk-free rate = 0 (For futures options)
 *
 ******************* FORMULA *********************
 *
 *     sqrt(2pi/T) *
 *             [
 *                 ( C - (S-ke^{-rT}/2) )
 *                         /
 *                 ( S - (S-ke^{-rT}/2) )
 *             ]
 *
 *************************************************/

#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <stdio.h>
#include <time.h>
#include <math.h>
#include <string.h>

#include "Maxfiles.h"
#include "MaxSLiCInterface.h"
#define pi 3.1415927
#define PI 3.14159265358979323846
#define tolerance 0.00001
#define epsilon 0.0001

int main(void)
{


    // UNDERLYING S
    const float S0 = 2124.50;

    // TERM 1 MONTH
    float term = 1.0/12.0;

    // FIRST TERM OF THE BCS FORMULA
    const float root = sqrt((2*pi)/term);

    // ALLOCATE MEMORY FOR FLOAT
    const int size = 8; // ARRAY SIZE
    int sizeBytesf = size * sizeof(float);

    float *sigma = malloc(sizeBytesf);
    float *k = malloc(sizeBytesf);
    float *c = malloc(sizeBytesf);
    float *CPU_sigma=malloc(sizeBytesf);

    // STRIKE
    //float x[5]= {
            int x[5]= {
                    2110,
                    2115,
                    2120,
                    2125,
                    2130
                };

    // OPTION MARKET PRICE
    float y[5]= {
                33.25,
                30.00,
                27.00,
                23.75,
                20.75
            };

    //    DATA TRANSFER
    for(int i = 0; i < 5; ++i) {

        // STRIKE PRICE
        k[i] = x[i];

        // OPTION PRICE
        c[i] = y[i];
    }


    // CPU COMPUTATIONS

    printf("*********** CONSTANTS ****************\n");

    printf("root = %f\n",root);
    printf("term = %f\n",term);

    // CPU COMPUTATIONS
    printf("*********** Running on CPU  ****************\n");

    for(int i = 0; i < 5; ++i)
        CPU_sigma[i]= root * (c[i] - (k[i] - S0 ) / 2.0) / (S0 - (S0 - k[i]) / 2.0);

    for(int i = 0; i < 5; ++i)
           printf("CPU_sigma[%i]= %f\n",i,CPU_sigma[i]);

    // DFE COMPUTATIONS
    printf("*********** Running on DFE ***********\n");
    //    SQRT ROOT
    float m = root;
    //    UNDERLYING 2124.50
    float l = S0;

    // DATA TO DFE
    BCS_Model(m,l, size, k, c, sigma);

    // PRINT DFE RESULT
    for(int i = 0; i < 5; ++i)
        printf("sigma[%i]= %f\n",i,sigma[i]);

    printf("Done.\n");
    return 0;
}
