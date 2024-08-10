double numero;
int numero2;
bool validador = true;
while (validador == true)
{
    Console.Write("Digite um número inteiro:");
    numero = Convert.ToDouble(Console.ReadLine());
    numero2 = (int)numero;
    if (numero2 != numero)
    {
        Console.WriteLine("Erro! Não é um número inteiro");
    }
    else
    {
        validador = false;
    }
}
Console.Write("O número é inteiro ");


