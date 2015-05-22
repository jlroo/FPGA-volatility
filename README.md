##Introduction

A joint collaboration between the Finance and Financial Service and Business Analytics Lab of Loyola University Chicago. The goal of this project is to compare the time taken to compute the price of a binomial option by using Coxâ€“Rossâ€“Rubinstein (CRR) procedure, followed by backward induction calculated on a regular machine against one that uses Maxeler computing. In Computer Science, a similar way of approach is by the **triangular wavefront** programming.

##Features of Maxeler Computing

-   ###Field-Programmable Gate Array (FPGA)

    Maxeler typically packs the Field-Programmable Gate Array (FPGA), made by Xilinx, in circuit boards that work with Intel Xeon microprocessors, whose unique chip allows for single and multiple data streams. Unlike traditional Read-Only Memory (ROM), the circuitry of these FPGAs gives the programmers flexibility to reconfigure data after it has been programmed into the chip.  It also supplies special software, called compilers, to exploit the hardware â€“ which can be continuously reprogrammed for customization to fit new sets of jobs. 

	
	
-   ###Multiscale Dataï¬‚ow Computing

    In addition to the FPGA, Maxelerâ€™s Multiscale Dataï¬‚owComputing, a combination of traditional synchronous data flow, vector and array processor, allows for the manipulation of loop level parallelism in a spatial, pipelined way. Large streams of data ï¬‚owing through a sea of arithmetic units can be connected to match the structure of the computer task. Small on-chip memories form a distributed register ï¬le with as many access ports as needed to support a smooth ï¬‚ow of data through the chip.

    Multiscale Dataï¬‚ow Computing employs dataï¬‚ow on multiple levels of abstraction: the system level,the architecture level, the arithmetic level and the bit level. On the system level, multiple dataï¬‚owengines are connected to form a supercomputer. On the architecture level, memory access can be decoupled from arithmetic operations, while the arithmetic and bit levels provide opportunities to optimize the representation of the data and balance computation with communication.

	`Figure 1: An example of Dataflow Computing in Maxeler`


![Programmingwith MaxCompiler](https://github.com/davidtanluc/ParallelProgramming/raw/master/wiki1/MaxelerSlide9%20.PNG)


-   As illustrated in Figure 1, CPU codes (C language) are the main front to navigate data streaming into Engine Codes (MyKernel in Figure 1), which are written in Java for its data design expressions. The communication and translation between CPU codes and Engine codes are performed continuously. 


##Our approach
The Coxâ€“Rossâ€“Rubinstein (CRR) procedure, part of the Binomal Options Pricing Model, was provided in Excel VBA format by the Finance Department. Computer Science Department converted these codes in Java for readability which would serve as a central point of reference between the original Excel VBA and the translated C language, which Maxeler computing exclusively uses for its CPU codes.

We built a controlled environment based on static data provided by the Finance Department. The static data comprises of spot prices, dividend yields and other constants of a standard call option. Using the CRR formula, these data were used to calculate Implied Volatility (IV).

##Our findings
The static data provided included call options with varying levels of precision in the CRR computations. The program was dependent on the different levels of precisions, ranging from 50 to 1000 times, in steps of 50.

Figure 2 shows the speed of calculating IV using Excel VBA on a PC and using the CPU codes on Maxeler. The time required by Maxeler to calculate the varying levels of precision varies from 0.001 to 0.079 seconds. This is sharply contrasted against the traditional Excel VBA on PC, which yielded times ranging from 0.971 to 95.835 seconds.


`Figure 2: Test Result`

![Build Status](https://github.com/davidtanluc/ParallelProgramming/raw/master/wiki1/Test1.png)



##Conclusion
The Maxeler technology is highly robust and versatile. It is able to substantially reduce processing times. Future tests should include large data sets with continuous data streaming that requires multiple loop iterations to further test the limitations, if any, of the Maxeler technology.


Latest release (Sun June 29 11:36:52 CST 2014)



- PDF : [FinalSummary](https://bytebucket.org/davidtanloyola/cs490s14teamcme/raw/6d52922c31132d5667ed41b6cb7be04f8561718d/DevelopmentLifeCycle/CycleThree/7%2C8%20Testing%2C%20PostMortem/CMEProjectFinalSummary.pdf?token=465ec1e7a6ae59eaba0aea41363e6b9b64a879ce)
