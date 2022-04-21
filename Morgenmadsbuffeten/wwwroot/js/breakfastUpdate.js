
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/reloadHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

    // Start the connection.
    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.on("Reload", function (message) {
        window.location.reload("KitchenGet");
    })

    connection.onclose(async () => {
        await start();
    });

    start();
