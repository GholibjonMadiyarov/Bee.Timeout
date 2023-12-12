# Bee.Timeout
A very simple way to work with the Timer

## How use?

```csharp
using Bee.Timeout;

static void Main(string[] args)
{
	Timeout.run(15000, callback);
}

private void callback()
{
    MessageBox.Show("Completed");
}
```