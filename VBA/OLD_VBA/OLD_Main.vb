Rem Attribute VBA_ModuleType=VBAModule
Sub Main
Rem Option Explicit
Rem 
Rem   Dim S As Double
Rem   Dim r As Double
Rem   Dim T As Double
Rem   Dim K As Double
Rem   Dim CallPutType As String
Rem   Dim ExerciseFlag As String
Rem   Dim ntimeSteps As Integer
Rem   Dim q As Double
Rem   Dim mktP As Double
Rem   Dim implied_volatility As Double
Rem   Dim check_price As Double
Rem   
Rem Public Sub Main()
Rem     
Rem   Sheets("sheet1").Range("D19").ClearContents
Rem   Sheets("sheet1").Range("D20").ClearContents
Rem   Sheets("sheet1").Range("D21").ClearContents
Rem   
Rem   S = Sheets("sheet1").Range("D8").Value
Rem   r = Sheets("sheet1").Range("D9").Value
Rem   T = Sheets("sheet1").Range("D10").Value
Rem   K = Sheets("sheet1").Range("D11").Value
Rem   CallPutType = Sheets("sheet1").Range("D12").Value
Rem   ExerciseFlag = Sheets("sheet1").Range("D13").Value
Rem   ntimeSteps = Sheets("sheet1").Range("D14").Value
Rem   q = Sheets("sheet1").Range("D15").Value
Rem   mktP = Sheets("sheet1").Range("D16").Value
Rem   
Rem   implied_volatility = IV(CallPutType, S, K, T, r, ExerciseFlag, ntimeSteps, q, mktP)
Rem   Sheets("sheet1").Range("D20").Value = implied_volatility
Rem   
Rem   If implied_volatility > 0 Then
Rem     check_price = CRR_Price(CallPutType, S, K, T, r, implied_volatility, ExerciseFlag, ntimeSteps, q)
Rem     Sheets("sheet1").Range("D19").Value = check_price
Rem   Else
Rem     Sheets("sheet1").Range("D19").Value = ""
Rem   End If
Rem   
Rem End Sub
Rem 
End Sub