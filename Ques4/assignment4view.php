<!DOCTYPE html>
<html>
<head>
    <title>User List</title>
    <style>
        body {
            background-color: #212121;
            color: #fff;
             font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        table {
            border-collapse: collapse;
            width: 100%;
            background-color: #333;
            color: #fff;
        }
        th, td {
            border: 1px solid #777;
            text-align: left;
            padding: 8px;
        }
        th {
            background-color: #444;
        }
        .fetch-btn {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            transition: background-color 0.3s ease;
        }
        .fetch-btn:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>

<h2>User List</h2>

<?php if (!empty($details)): ?>
    <table>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Fetch Linked Accounts</th>
        </tr>
        <?php foreach ($details as $user): ?>
            <tr>
                <td><?php echo $user['userid']; ?></td>
                <td><?php echo $user['fullname']; ?></td>
                <td><?php echo $user['email']; ?></td>
                <td><button class="fetch-btn" onclick=loadOtherAccounts(<?php echo $user['userid']; ?>)>More accounts</td>
            </tr>
        <?php endforeach; ?>
    </table>
<?php else: ?>
    <p>No users found.</p>
<?php endif; ?>

<script>
    
    async function loadOtherAccounts(id)
    {
        const result = await fetch('assignment4/fetchAccountData/'+id);
        const result_json = await result.json();
        console.log(result_json);

        // display accounts table
        const heading = document.createElement('h2');
        heading.textContent = "Linked Accounts";
        document.body.appendChild(heading);
        
        const table = document.createElement('table');
        const headerRow = table.insertRow();
        const table_headers = ['ID', 'Email', 'Account Id', 'Account Name'];
        
        table_headers.forEach(header => {
            const th = document.createElement('th');
            th.textContent = header;
            headerRow.appendChild(th);
        });

        result_json.forEach(account => {
            const row = table.insertRow();
            row.insertCell().textContent = account.userid;
            row.insertCell().textContent = account.email;
            row.insertCell().textContent = account.accountid;
            row.insertCell().textContent = account.accountname;
        });

        document.body.appendChild(table);
    }

</script>

</body>
</html>
