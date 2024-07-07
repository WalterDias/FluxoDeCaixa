
using BenchmarkDotNet.Running;
using FluxoDeCaixa.Testes.Benchmark;

//var cultura = new CultureInfo("pt-BR");

//double i = 14.3;
//DateTime data = DateTime.Now;

//Console.WriteLine($"{i.ToString(cultura)} {data.ToString(cultura)}");

BenchmarkRunner.Run<Benchmarks>();