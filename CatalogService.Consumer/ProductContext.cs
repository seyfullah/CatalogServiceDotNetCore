
using FlowardApi.Model;

public class ProductContext
{
    public ProductContext()
    {
    }


    public void Send(Products model)
    {
        System.Console.WriteLine(DateTime.Now.ToString() + " Send e-mail for " + model.Name);
    }
}