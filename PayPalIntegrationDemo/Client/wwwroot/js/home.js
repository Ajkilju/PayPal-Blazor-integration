var paypalButtons = paypal.Buttons({

    createOrder() {
        let amount = document.getElementById('amount-input').value
        return fetch("/paypal/create-order", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                amount: amount
            }),
        })
        .then((response) => response.json())
        .then((order) => order.id);
    },

    onApprove(data) {
        return fetch("/paypal/capture-order", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                orderID: data.orderID
            })
        })
        .then((response) => response.json())
        .then((orderData) => {
            console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));
            const infoContainer = document.getElementById('paypal-info-container');
            infoContainer.innerHTML = '<div class="alert alert-success" role="alert">Success! Thank you for your payment!</div >';
        });
    }

});

renderPaypalButtons = () => {
    paypalButtons.render('#paypal-button-container');
}