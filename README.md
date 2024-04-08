# Bee.Timeout
A very simple way to work with the Timeout

## How use?

```csharp
using Bee.Timeout;

static void Main(string[] args)
{
    // 15 seconds
    Timeout.run(15, callback);
}

private void callback()
{
    MessageBox.Show("Completed");
}
```
