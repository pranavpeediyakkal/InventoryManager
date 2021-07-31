Tracking:
data store design: 10min
API service logic: 30min
Extra handling like logging and pagination: 1H
Unit test coverage: 2H

Note: I have removed all the UI related code from the default application.
Just run/host the application and call the api.

Steps for installation:
1. Fire the InventoryTable.sql queries.
2. Update "InventoryConnectionString" on Web.config
3. Publish the applicaiton
4. Call the api

APIs:
1. GetItems
Get the list of items present in the inventory.
https://localhost:44326/api/inventory/GetItems/1/10/?search=stop
https://localhost:44326/api/inventory/GetItems/1/10

2. UpdateItem
Update an item in the inventory.
https://localhost:44326/api/inventory/AddItem

3. AddItem
Add an item into the inventory.
https://localhost:44326/api/inventory/UpdateItem

4. DeleteItem
Delete an item from inventory.
https://localhost:44326/api/inventory/DeleteItem/5