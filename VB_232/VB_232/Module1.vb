Module Module1
    ' --------------------------- Variáveis Globais -----------------------------------------
    Public Port As String
    Public Baud_Rate As String
    Public NewConfigAvailable As Boolean

    'Public rx As String

    Public NomeFicheiro As String
    Public MensagemFicheiro() As String
    'Public LocalizaçãoFicheiro As String
    Public NewFile As Boolean

    ' --------------------------- Função para calcular o BCC -----------------------------------------
    'Uma tipica mensagem em LSV2 é algo como <DLE><STX>telegrama<DLE><ETX><BCC>
    'Para o cálculo Do BCC os caracteres <DLE><STX> não contam
    'O input da função é o telegrama a ser convertido sem contar com os caracteres especiais
    'No incio da função convertemos tudo para letras maiusculas para evitar erros de comparação uma vez que é Case sensitive
    'O operador Xor (bitwise EXCLUSIVE Or) faz a comparação entre os respetivos bits, neste caso, de dois caracteres
    '0  0  0  0  0  0  1  1    caracter 1
    '0  0  0  0  0  1  0  1    caracter 2
    '----------------------
    '0  0  0  0  0  1  1  0     Result
    '   No loop faz-se a comparação do telegrama
    '  No final faz-se a comparação com o Decimal 3 correspondente ao caracter <ETX>
    ' A função retorna o Decimal Do caracter correspondente à tabela ASCII
    Public Function BCC(StrToConvert As String)


        ' Converter tudo para upper case uma vez que os integers para caracteres minusculos são diferentes
        Dim result As Integer
        StrToConvert = StrToConvert.ToUpper()

        ' fazer a comparação de bits entre os diferentes caracteres
        For i As Integer = 0 To Len(StrToConvert) - 1

            result = result Xor Asc(StrToConvert(i))

        Next

        ' fazer a ultima comparação com <ETX> que é decimal 3
        result = result Xor 3

        ' a função devolve o decimal do BCC
        Return result


    End Function

End Module
