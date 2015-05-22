Rem Attribute VBA_ModuleType=VBAModule
Sub Functions
Rem 
Rem Option Explicit
Rem 
Rem   Dim S As Double
Rem   Dim S0 As Double
Rem   Dim r As Double
Rem   Dim T As Double
Rem   Dim K As Double
Rem   Dim X As Double
Rem   Dim CallPutType As String
Rem   Dim ExerciseFlag As String
Rem   Dim ntimeSteps As Integer
Rem   Dim q As Double
Rem   Dim mktP As Double
Rem   Dim dt As Double
Rem   Dim u As Double
Rem   Dim d As Double
Rem   Dim i As Integer
Rem   Dim j As Integer
Rem   Dim present_value As Double
Rem   Dim immediate_val As Double
Rem   Dim p1 As Double
Rem   Dim p2 As Double
Rem   Dim check As Double
Rem   Dim v1 As Double
Rem   Dim v2 As Double
Rem   Dim tolerance As Double
Rem   Dim epsilon As Double
Rem   Dim counter As Integer
Rem   Dim value1 As Double
Rem   Dim value2 As Double
Rem   Dim diff As Double
Rem   Dim dx As Double
Rem 
Rem 'This function returns option price using CRR Binomial tree
Rem 'parameters are:
Rem 'CallPutFlag - use "call" for call and "put" for put option
Rem 'S - spot price
Rem 'K - option strike
Rem 'T - option maturity
Rem 'r - risk free rate
Rem 'ExerciseType - use "Am" for American and "Eu" for european
Rem 'N - no of time steps for the binomial tree
Rem 'q - dividend yield
Rem 
Rem Public Function CRR_Price(CallPutFlag, S, K, T, r, v, ExerciseType, N, q) As Double
Rem   
Rem   S0 = S
Rem   If CallPutFlag = "call" Then
Rem     CallPutType = 1
Rem   Else
Rem     CallPutType = -1
Rem   End If
Rem   dt = T / N
Rem   u = Exp(v * dt ^ 0.5) 'size of up jump
Rem   d = Exp(-v * dt ^ 0.5) 'size of down jump
Rem   p1 = (Exp((r - q) * dt) - d) / (u - d) 'probability of up jump
Rem   p2 = 1 - p1 'probability of down jump
Rem   ReDim Smat(1 To N + 1, 1 To N + 1)
Rem   Smat(1, 1) = S0
Rem   For i = 1 To UBound(Smat, 1) - 1
Rem     Smat(1, i + 1) = Smat(1, i) * Exp(v * dt ^ 0.5)
Rem     For j = 2 To i + 1
Rem       Smat(j, i + 1) = Smat(j - 1, i) * Exp(-v * dt ^ 0.5)
Rem     Next j
Rem   Next i
Rem   
Rem   ReDim Cmat(1 To N + 1, 1 To N + 1)
Rem   
Rem   For i = 1 To N + 1
Rem   Cmat(i, N + 1) = Application.Max(CallPutType * (Smat(i, N + 1) - K), 0)
Rem   Next i
Rem   For i = UBound(Smat, 2) - 1 To 1 Step -1
Rem     For j = 1 To i
Rem       present_value = Exp(-r * dt) * (p1 * Cmat(j, i + 1) + p2 * Cmat(j + 1, i + 1))
Rem       immediate_val = CallPutType * (Smat(j, i) - K)
Rem       If ExerciseType = "Am" Then
Rem         Cmat(j, i) = Application.Max(present_value, immediate_val)
Rem       Else
Rem         Cmat(j, i) = Application.Max(present_value, 0)
Rem       End If
Rem     Next j
Rem   Next i
Rem   CRR_Price = Cmat(1, 1)
Rem End Function
Rem 
Rem 'This function returns Implied volatility using CRR Binomial tree
Rem 'added parameter:
Rem 'mktP - option premium
Rem 
Rem Public Function IV(CallPutFlag, S, K, T, r, ExerciseType, N, q, mktP) As Double
Rem   
Rem   S0 = S
Rem   If CallPutFlag = "call" Then
Rem     CallPutType = 1
Rem   Else
Rem     CallPutType = -1
Rem   End If
Rem   
Rem 'Implied Volatility Approximation
Rem 
Rem   X = K * Exp(-r * T)
Rem   
Rem   Select Case CallPutType
Rem   Case 1
Rem     check = (mktP - (S - X) / 2) ^ 2 - (S - X) ^ 2 / WorksheetFunction.Pi
Rem     If check >= 0 Then
Rem       v1 = Sqr(2 * WorksheetFunction.Pi / T) * 1 / (S + X) * (mktP - (S - X) / 2 + Sqr((mktP - (S - X) / 2) ^ 2 - (S - X) ^ 2 / WorksheetFunction.Pi)) 'Corrado-Miller
Rem     Else
Rem       v1 = Sqr(2 * WorksheetFunction.Pi / T) * (mktP - (S - X) / 2) / (S - (S - X) / 2) 'Bharadia-Christopher-Salkin
Rem     End If
Rem   Case -1
Rem     check = (mktP - (X - S) / 2) ^ 2 - (X - S) ^ 2 / WorksheetFunction.Pi
Rem     If check >= 0 Then
Rem       v1 = Sqr(2 * WorksheetFunction.Pi / T) * 1 / (S + X) * (mktP - (X - S) / 2 + Sqr((mktP - (X - S) / 2) ^ 2 - (X - S) ^ 2 / WorksheetFunction.Pi)) 'Corrado-Miller
Rem     Else
Rem       v1 = Sqr(2 * WorksheetFunction.Pi / T) * (mktP - (X - S) / 2) / (X - (X - S) / 2) 'Bharadia-Christopher-Salkin
Rem     End If
Rem   End Select
Rem   
Rem   If v1 <= 0 Then
Rem      MsgBox ("Volatility <= 0, please input new parameters")
Rem   Else
Rem 
Rem 'Newton Raphson
Rem 
Rem     tolerance = 0.00001
Rem     epsilon = 0.0001
Rem     counter = 0
Rem   
Rem     Do
Rem     value1 = CRR_Price(CallPutFlag, S0, K, T, r, v1, ExerciseType, N, q)
Rem     diff = mktP - value1
Rem     If Abs(diff) <= tolerance Then Exit Do
Rem     v2 = v1 - epsilon
Rem     value2 = CRR_Price(CallPutFlag, S0, K, T, r, v2, ExerciseType, N, q)
Rem     dx = (value2 - value1) / epsilon
Rem     v1 = v1 - (mktP - value1) / dx
Rem       If v1 <= 0 Then
Rem          MsgBox ("Volatility <= 0, please input new parameters")
Rem          Exit Do
Rem       End If
Rem     counter = counter + 1
Rem     Loop
Rem     IV = v1
Rem     Sheets("sheet1").Range("D21").Value = counter
Rem   
Rem   End If
Rem    
Rem End Function
End Sub