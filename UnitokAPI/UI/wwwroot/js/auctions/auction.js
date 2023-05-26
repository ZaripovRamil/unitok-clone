"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/auctionHub").build();

const auctionId = document.getElementById('auction-id').value;
const userId = document.getElementById('user-id').value;
let userBalanceElem = document.getElementById('user-balance');
let bidResultElem = document.getElementById('bid-result');
const userBalanceBeforeAuction = +userBalanceElem.textContent;
document.getElementById("placeBid").disabled = true;

connection.on("ReceiveBid", (bid) => {
    let li = document.createElement("li");
    document.getElementById("bidsList").appendChild(li);
    // beware of xss
    li.textContent = `${bid.name} places ${bid.bidValue}`;
    if (bid.bidderId === userId) {
        userBalanceElem.textContent = (userBalanceBeforeAuction-bid.bidValue).toFixed(2);
    } else {
        userBalanceElem.textContent = userBalanceBeforeAuction.toFixed(2);
    }
});

connection.start()
    .then(() => {
        return connection.invoke("JoinAuction", auctionId)
            .then(res => {
                if (!res.isSuccessful) {
                    bidResultElem.innerHTML = res.resultMessage;
                }
                return res;
            })
    })
    .then(r => {
        if (!r.isSuccessful) return r;
        return connection.invoke("SendBidsHistory", auctionId)
            .then(res => {
                if (!res.isSuccessful) {
                    bidResultElem.innerHTML = res.resultMessage;
                }
                return res;
            })
    })
    .then(r => {
        if (!r.isSuccessful) return r;
        document.getElementById("placeBid").disabled = false;
    })
    .catch((err) => console.error(err.toString()));

document.getElementById("placeBid").addEventListener("click", (event) => {
    const bid = +document.getElementById("bidValue").value;
    bidResultElem.innerHTML = "";
    connection.invoke("PlaceBid", auctionId, bid)
        .then(res => {
            if (!res.isSuccessful) {
                bidResultElem.innerHTML = res.resultMessage;
            }
            return res;
        })
        .catch((err) => console.error(err.toString()));
    event.preventDefault();
});