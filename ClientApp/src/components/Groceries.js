import React from "react";
import { Input, Table, Form, Button } from "reactstrap";
import { useState } from "react";

export function Groceries() {
    const [groceries, setGroceries] = useState([]);
    const [addFormData, setAddFormData] = useState({
        store: "",
        item: "",
        price: "",
        date: "",
    });

    const handleAddFormChange = (event) => {
        event.preventDefault();
        const fieldName = event.target.getAttribute("name");
        const fieldValue = event.target.value;

        const newFormData = { ...addFormData };
        newFormData[fieldName] = fieldValue;

        setAddFormData(newFormData);
    };

    const handleAddFormSubmit = (event) => {
        event.preventDefault();

        const newGrocery = {
            store: addFormData.store,
            item: addFormData.item,
            price: addFormData.price,
            date: addFormData.date,
        };

        const newGroceries = [...groceries, newGrocery];
        setGroceries(newGroceries);
    };

    return (
        <div class="bg-secondary rounded h-100 p-4">
            <Form onSubmit={handleAddFormSubmit}>
                <label>Store Name</label>
                <select
                    required
                    class="form-select"
                    name="store"
                    onChange={handleAddFormChange}
                >
                    <option></option>
                    <option>Iki</option>
                    <option>Lidl</option>
                    <option>Maxima</option>
                    <option>Norfa</option>
                    <option>Rimi</option>
                </select>
                <label>Item Name</label>
                <Input
                    required
                    type="text"
                    name="item"
                    onChange={handleAddFormChange}
                />
                <label>Item Price</label>
                <Input
                    required
                    type="number"
                    step="0.01"
                    name="price"
                    onChange={handleAddFormChange}
                />
                <label>Purchase Date</label>
                <Input
                    required
                    type="date"
                    name="date"
                    onChange={handleAddFormChange}
                />
                <Button color="primary">Save</Button>
            </Form>
            <Table>
                <thead>
                    <tr>
                        <th>Store</th>
                        <th>Item</th>
                        <th>Price</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {groceries.map((data, i) => (
                        <tr key={i}>
                            <td>{data.store}</td>
                            <td>{data.item}</td>
                            <td>{data.price}</td>
                            <td>{data.date}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
}
