const USER_ID = 2;
const ACCOUNT_ID = 1425022273;

function fetchData() {
    let url = `https://localhost:7148/GetCPReport/${USER_ID}/${ACCOUNT_ID}`;
    try {
        fetch(url).then((data) => {
            return data.json();
        }).then((data) => {
            const tableBody = document.querySelector('#campaign-table tbody');
            data.forEach(item => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.clicks}</td>
                        <td>${item.impressions}</td>
                        <td>${item.cost}</td>
                    `;
                tableBody.appendChild(newRow);
            });
        })
        // if (!response.ok) {
        //     throw "Something went wrong...";
        // }
        console.log(response);

    } catch (err) {
        console.log(err);
    }
}

function downloadPdf() {
    let url = `https://localhost:7148/createpdf`;

    try {
        fetch(url);
    } catch (err) {
        alert(err);
    }
}



fetchData();