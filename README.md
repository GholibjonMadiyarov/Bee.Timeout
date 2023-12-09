# Bee.Timet
A very simple way to work with the Timer

## How use?

```csharp
using Bee.Timer;

static void Main(string[] args)
{
	Timer.run(15000, callback);
}

private void callback()
{
    MessageBox.Show("Completed");
}
```