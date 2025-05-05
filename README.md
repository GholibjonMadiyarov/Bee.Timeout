# Bee.Timeout
A very simple way to work with the Timeout

## How use?

```csharp
using Bee.Timeout;

static void Main(string[] args)
{
    Timeout timeout;
    timeout = new Timeout();

    // 30 seconds
    timeout.start(30, callback);

    // Can be refresh if needed
    timeout.refresh(60);

    //Call this method when the session ends. In Windows Forms or WPF, use the Unload event handler.
    timeout.close();
    
    // 15 seconds
    Timeout.sleep(15, callback);
}

private void callback()
{
    MessageBox.Show("Completed");
}
```
