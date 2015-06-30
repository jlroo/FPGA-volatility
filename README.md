##Introduction

A joint collaboration between the Finance Department, The Financial Service and Business Analytics Lab at Loyola University Chicago and The CME Group. The goal of this project is to compare the time taken to compute the price of a binomial option by using The Cox–Ross–Rubinstein (CRR) procedure, followed by backward induction calculated on a regular machine against one that uses Maxeler computing. In Computer Science, a similar way of approach is by the **triangular wavefront** programming.

##Features of Maxeler Computing

-   **Field-Programmable Gate Array (FPGA)**

    Maxeler typically packs the Field-Programmable Gate Array (FPGA), made by Xilinx, in circuit boards that work with Intel Xeon microprocessors, whose unique chip allows for single and multiple data streams. Unlike traditional Read-Only Memory (ROM), the circuitry of these FPGAs gives the programmers flexibility to reconfigure data after it has been programmed into the chip.  It also supplies special software, called compilers, to exploit the hardware â€“ which can be continuously reprogrammed for customization to fit new sets of jobs. 
  
  
-   **Multiscale Dataﬂow Computing**

  In addition to the FPGA, Maxeler’s Multiscale DataﬂowComputing, a combination of traditional synchronous data flow, vector and array processor, allows for the manipulation of loop level parallelism in a spatial, pipelined way. Large streams of data ﬂowing through a sea of arithmetic units can be connected to match the structure of the computer task. Small on-chip memories form a distributed register ﬁle with as many access ports as needed to support a smooth ﬂow of data through the chip.

  Multiscale Dataﬂow Computing employs dataﬂow on multiple levels of abstraction: the system level,the architecture level, the arithmetic level and the bit level. On the system level, multiple dataﬂowengines are connected to form a supercomputer. On the architecture level, memory access can be decoupled from arithmetic operations, while the arithmetic and bit levels provide opportunities to optimize the representation of the data and balance computation with communication.

|Figure 1: An example of Dataflow Computing in Maxeler|
|:-------------:|
|<img height="95%" width="95%" src="https://raw.githubusercontent.com/jlroo/maxeler/master/IMG/MAXCOMPILER.png">|
| As illustrated in Figure 1, CPU codes (C language) are the main front to navigate data streaming into Engine Codes (MyKernel in Figure 1), which are written in Java for its data design expressions. The communication and translation between CPU codes and Engine codes are performed continuously.|


##Our approach

The Cox–Ross–Rubinstein (CRR) procedure, part of the Binomal Options Pricing Model, was provided in Excel VBA format by the Finance Department. The Computer Science Department converted these codes in Java for readability which would serve as a central point of reference between the original Excel VBA and the translated C language, which Maxeler computing exclusively uses for its CPU codes. The Financial Service and Business Analytics Lab covered the Java and C code to Maxeler Code.

Initially we built a controlled environment based on static data provided by the Finance Department (VBA model). The static data comprises of spot prices, dividend yields and other constants of a standard call option. Using the CRR formula, these data were used to calculate Implied Volatility (IV).

|Figure 2: Program  Flowchart|
|:-------------:|
|<img height="95%" width="95%" src="https://raw.githubusercontent.com/jlroo/maxeler/master/IMG/flowchart.png">|

|Binomial Price Model| Calculation |
|:-------------:|:-------------:|
|<img height="95%" width="95%" src="https://raw.githubusercontent.com/jlroo/maxeler/master/IMG/TREE_PRICE.png">|<img height="95%" width="95%" src="https://raw.githubusercontent.com/jlroo/maxeler/master/IMG/crr_tree.png">|

##Practical Implication

* Implied volatility outperforms time-series models based on historical data for the purposes of forecasting volatility.
* Volatility is an important input into VAR and other models.  Relevant to all money managers.
* Using CME’s S\&P500 futures options (minis) we have the highest quality data thereby maximizing efficacy.

##Next Steps

* Program the Newton-Raphson algorithm to run in the DFE.
* Bring in a time dimension to the problem (estimating a vol surface instead of a smile).
* Migrate all calculations to fixed point.
* Consider other approaches that might better exploit the DFE (e.g., Monte Carlo).
* Create something akin to the VIX using CME contracts?

##Resources

**CRR Model Paper:** <a href="https://www.researchgate.net/publication/279296767_Implied_Volatility_for_Options_on_Futures_Using_the_Cox-Ross-Rubinstein_%28CRR%29_Model" target="_blank"> Implied Volatility for Options on Futures Using the Cox-Ross-Rubinstein (CRR) Model</a>
