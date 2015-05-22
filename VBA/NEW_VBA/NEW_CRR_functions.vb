
Option Explicit

  Dim S As Double
  Dim S0 As Double
  'Dim F0 As Double'
  'Dim F As Double'
  Dim r As Double
  Dim T As Double
  Dim K As Double
  Dim X As Double
  Dim CallPutType As String
  Dim ExerciseFlag As String
  Dim ntimeSteps As Integer
  Dim q As Double
  Dim mktP As Double
  Dim dt As Double
  Dim u As Double
  Dim d As Double
  Dim i As Integer
  Dim j As Integer
  Dim present_value As Double
  Dim immediate_val As Double
  Dim p1 As Double
  Dim p2 As Double
  Dim check As Double
  Dim v1 As Double
  Dim v2 As Double
  Dim tolerance As Double
  Dim epsilon As Double
  Dim counter As Integer
  Dim value1 As Double
  Dim value2 As Double
  Dim diff As Double
  Dim dx As Double

'This function returns option price using CRR Binomial tree'
'parameters are:'
'CallPutFlag - use "call" for call and "put" for put option'
'S - spot price'
'K - option strike'
'T - option maturity'
'r - risk free rate'
'ExerciseType - use "Am" for American and "Eu" for european'
'N - no of time steps for the binomial tree'
'q - dividend yield'

Public Function CRR_Price(CallPutFlag, S, K, T, r, v, ExerciseType, N, q) As Double
  
  S0 = S
  If CallPutFlag = "call" Then
    CallPutType = 1
  Else
    CallPutType = -1
  End If
  dt = T / N
  u = Exp(v * dt ^ 0.5) 'size of up jump'
  d = Exp(-v * dt ^ 0.5) 'size of down jump'
  p1 = (Exp((r - q) * dt) - d) / (u - d) 'probability of up jump'
  p2 = 1 - p1 'probability of down jump'
  ReDim Smat(1 To N + 1, 1 To N + 1)
  Smat(1, 1) = S0
  For i = 1 To UBound(Smat, 1) - 1
    Smat(1, i + 1) = Smat(1, i) * Exp(v * dt ^ 0.5)
    For j = 2 To i + 1
      Smat(j, i + 1) = Smat(j - 1, i) * Exp(-v * dt ^ 0.5)
    Next j
  Next i
  
  ReDim Cmat(1 To N + 1, 1 To N + 1)
  
  For i = 1 To N + 1
  Cmat(i, N + 1) = Application.Max(CallPutType * (Smat(i, N + 1) - K), 0)
  Next i
  For i = UBound(Smat, 2) - 1 To 1 Step -1
    For j = 1 To i
      present_value = Exp(-r * dt) * (p1 * Cmat(j, i + 1) + p2 * Cmat(j + 1, i + 1))
      immediate_val = CallPutType * (Smat(j, i) - K)
      If ExerciseType = "Am" Then
        Cmat(j, i) = Application.Max(present_value, immediate_val)
      Else
        Cmat(j, i) = Application.Max(present_value, 0)
      End If
    Next j
  Next i
  CRR_Price = Cmat(1, 1)
End Function

'This function returns Implied volatility using CRR Binomial tree'
'added parameter:'
'mktP - option premium'

Public Function IV(CallPutFlag, S, K, T, r, ExerciseType, N, q, mktP) As Double
  
  S0 = S
  If CallPutFlag = "call" Then
    CallPutType = 1
  Else
    CallPutType = -1
  End If
  
'Implied Volatility Approximation'

  X = K * Exp(-r * T)
  
  Select Case CallPutType
  Case 1
    check = (mktP - (S - X) / 2) ^ 2 - (S - X) ^ 2 / WorksheetFunction.Pi
    If check >= 0 Then
      v1 = Sqr(2 * WorksheetFunction.Pi / T) * 1 / (S + X) * (mktP - (S - X) / 2 + Sqr((mktP - (S - X) / 2) ^ 2 - (S - X) ^ 2 / WorksheetFunction.Pi)) 'Corrado-Miller'
    Else
      v1 = Sqr(2 * WorksheetFunction.Pi / T) * (mktP - (S - X) / 2) / (S - (S - X) / 2) 'Bharadia-Christopher-Salkin'
    End If
  Case -1
    check = (mktP - (X - S) / 2) ^ 2 - (X - S) ^ 2 / WorksheetFunction.Pi
    If check >= 0 Then
      v1 = Sqr(2 * WorksheetFunction.Pi / T) * 1 / (S + X) * (mktP - (X - S) / 2 + Sqr((mktP - (X - S) / 2) ^ 2 - (X - S) ^ 2 / WorksheetFunction.Pi)) 'Corrado-Miller'
    Else
      v1 = Sqr(2 * WorksheetFunction.Pi / T) * (mktP - (X - S) / 2) / (X - (X - S) / 2) 'Bharadia-Christopher-Salkin'
    End If
  End Select
  
  If v1 <= 0 Then
     MsgBox ("Volatility <= 0, please input new parameters")
  Else

'Newton Raphson'

    tolerance = 0.00001
    epsilon = 0.0001
    counter = 0
  
    Do
    value1 = CRR_Price(CallPutFlag, S0, K, T, r, v1, ExerciseType, N, q)
    diff = mktP - value1
    If Abs(diff) <= tolerance Then Exit Do
    v2 = v1 - epsilon
    value2 = CRR_Price(CallPutFlag, S0, K, T, r, v2, ExerciseType, N, q)
    dx = (value2 - value1) / epsilon
    v1 = v1 - (mktP - value1) / dx
      If v1 <= 0 Then
         MsgBox ("Volatility <= 0, please input new parameters")
         Exit Do
      End If
    counter = counter + 1
    Loop
    IV = v1
    Sheets("sheet1").Range("D21").Value = counter
  
  End If
   
End Function
