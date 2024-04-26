const USER_ID = 2;
const ACCOUNT_ID = 1425022273;

async function fetchData() {
    let url = `https://localhost:7148/GetCPReport/${USER_ID}/${ACCOUNT_ID}`;
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw "Something went wrong...";
        }

        const data = await response.json();
        console.log(data[0].id);

        const tableBody = document.querySelector('#campaign-table tbody');
        data.forEach(item => {
            const newRow = document.createElement('tr');
            newRow.innerHTML = `
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.status}</td>
                        <td>${item.biddingStrategy}</td>
                        <td>${item.type}</td>
                        <td>${item.servingStatus}</td>
                    `;
            tableBody.appendChild(newRow);
        });
    } catch (err) {
        console.log(err);
    }
}

async function downloadPdf()
{
    let url = `https://localhost:7148/createpdf`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw "Something went wrong...";
        }

        const data = await response.json();
        console.log(data)
        // if(data.)
    } catch (err) {
        alert(err);
    }
}


fetchData();