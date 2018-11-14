Public Class AdvCalc
    Inherits Calc

    Public Function GetAverage() As Double
        IF(n > 0) THEN
            GetAverage = sum / n
        Else
            GetAverage = 0
        End If
    END Function
END Class