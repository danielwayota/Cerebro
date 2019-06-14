# Cerebro: Simple Neuroevolution library

This is a very small library created to be used with Unity3D but it can be used standalone.

## Quick examples

### Network creation

#### Creating one cerebro

```c
Layer[] layers = new Layer[] {
    new Layer(2, 2, new Tanh()),
    new Layer(2, 1, new Sigmoid())
};

Cerebro brain = new Cerebro(layers);
brain.Initialize(10f);
```


#### Using factory pattern

*Use the factory to create one cerebro*
```c
Cerebro net = Factory.Create()
    .WithInput(2)
    .WithLayer(3, LayerType.Tanh)
    .WithLayer(1, LayerType.Sigmoid)
    .Build();
```

*Use a common factory to create several cerebros*
```c
Factory factory = Factory.Create()
    .WithInput(2)
    .WithLayer(3, LayerType.Tanh)
    .WithLayer(1, LayerType.Sigmoid)

Cerebro someNet = factory.Build();
Cerebro someDiferentNet = factory.Build();
```

#### Pseudo-genetics with neural networks

*Clone cerebros*
```c
Factory factory = Factory.Create()
    .WithInput(2)
    .WithLayer(3, LayerType.Tanh)
    .WithLayer(1, LayerType.Sigmoid)

Cerebro someNet = factory.Build();

Genome gen = someNet.GetGenome();

// Change 'genes' randomly
gen.Mutate(0.1f);

Cerebro clone = factory.WithGenome(gen).Build();
```

*Crossover*

```c
Factory factory = Factory.Create()
    .WithInput(2)
    .WithLayer(3, LayerType.Tanh)
    .WithLayer(1, LayerType.Sigmoid)

Cerebro parent1 = factory.Build();
Cerebro parent2 = factory.Build();

Genome gen1 = parent1.GetGenome();
Genome gen2 = parent1.GetGenome();

Genome gen3 = Genome.Crossover(gen1, gen2);
// Change 'genes' randomly
gen3.Mutate(0.1f);

Cerebro brain3 = factory.WithGenome(gen3).Build();
```