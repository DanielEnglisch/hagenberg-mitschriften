Public Class AdvCalc 
    Inherits Calc


    Public Function GetAverage() As Double
        If (n > 0) Then
            GetAverage = sum / n
        Else
            GetAverage = 0
        End If
    End Function
    
End Class