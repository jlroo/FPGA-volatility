Option Explicit

  'Dim F As Double'
  Dim S As Double
  Dim r As Double
  Dim T As Double
  Dim K As Double
  Dim CallPutType As String
  Dim ExerciseFlag As String
  Dim ntimeSteps As Integer
  Dim q As Double
  Dim mktP As Double
  Dim implied_volatility As Double
  Dim check_price As Double
  
Public Sub Main()
  'We use real life data from CMEGroup:'
  'Underline: E-mini S&P 500 June 2015 Futures'
  'Type: American Call Options'
  'Expiration: Jun 2015'
  'Source/Update: Globex 18:25:40 CT 21 May 2015'
  'Sheets("sheet1").Range("D19").ClearContents'
  'Sheets("sheet1").Range("D20").ClearContents'
  'Sheets("sheet1").Range("D21").ClearContents'
  S = Cells(1, 5)
  'F = Cells(1, 2)'
  r = 0
  T = 1 / 12
  Dim i As Integer
  Dim j As Integer
  For i = 1 To 36 Step 1
  K = Cells(i + 1, 1)
  CallPutType = "call"
  ExerciseFlag = "Am"
  ntimeSteps = 10
  q = 0
  mktP = Cells(i + 1, 2)
  
  'r = Sheets("sheet1").Range("D9").Value'
  'T = Sheets("sheet1").Range("D10").Value'
  'K = Sheets("sheet1").Range("D11").Value'
  'CallPutType = Sheets("sheet1").Range("D12").Value'
  'ExerciseFlag = Sheets("sheet1").Range("D13").Value'
  'ntimeSteps = Sheets("sheet1").Range("D14").Value'
  'q = Sheets("sheet1").Range("D15").Value'
  'mktP = Sheets("sheet1").Range("D16").Value'

  implied_volatility = IV(CallPutType, S, K, T, r, ExerciseFlag, ntimeSteps, q, mktP)
  Cells(i + 1, 2) = implied_volatility
  
  'If implied_volatility > 0 Then'
   ' check_price = CRR_Price(CallPutType, S, K, T, r, implied_volatility, ExerciseFlag, ntimeSteps, q)'
    'Cells(i + 1, 10) = check_price'
  'Else
    'Cells(i + 1, 10) = "" '
  'End If'
  Next i
End Sub

