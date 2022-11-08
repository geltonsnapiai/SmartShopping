import React, { useEffect } from "react";
import { Input, Table, Form, Button } from "reactstrap";
import { nanoid } from "nanoid";
import { useState, Fragment } from "react";
import ReadOnlyRow from "./ReadOnlyRow";
import EditableRow from "./EditableRow";
import { authFetch } from "../../auth/AuthFetch";

export function Groceries() {
    const [shops, setShops] = useState([]);
    const [groceries, setGroceries] = useState([]);

    const [addFormData, setAddFormData] = useState({
        store: "",
        item: "",
        price: "",
        date: "",
    });

    const [editFormData, setEditFormData] = useState({
        store: "",
        item: "",
        price: "",
        date: "",
      });

    const [editGroceryId, setEditGroceryId] = useState(null);


    const handleAddFormChange = (event) => {
        event.preventDefault();
        const fieldName = event.target.getAttribute("name");
        const fieldValue = event.target.value;

        const newFormData = { ...addFormData };
        newFormData[fieldName] = fieldValue;

        setAddFormData(newFormData);
    };

    const handleEditFormChange = (event) => {
        event.preventDefault();
        const fieldName = event.target.getAttribute("name");
        const fieldValue = event.target.value;

        const newFormData = { ...editFormData };
        newFormData[fieldName] = fieldValue;

        setEditFormData(newFormData);
    };

    const handleAddFormSubmit = (event) => {
        event.preventDefault();

        const newGrocery = {
            id: nanoid(),
            store: addFormData.store,
            item: addFormData.item,
            price: addFormData.price,
            date: addFormData.date,
        };

        const newGroceries = [...groceries, newGrocery];
        setGroceries(newGroceries);
    };

    const handleEditClickSubmit = (event) => {
        event.preventDefault();
        
        const editedGrocery = {
            id: editGroceryId,
            store: editFormData.store,
            item: editFormData.item,
            price: editFormData.price,
            date: editFormData.date,
          };
        
          const newGroceries = [ ...groceries ]

          const index = groceries.findIndex((grocery) => grocery.id === editGroceryId);

          newGroceries[index] = editedGrocery;

          setGroceries(newGroceries);
          setEditGroceryId(null);
    }

    const handleDeleteGrocery = (groceryId) => {
        const newGroceries= [...groceries];
        
        const index = groceries.findIndex((grocery)=> grocery.id === groceryId);

        newGroceries.splice(index, 1);

        setGroceries(newGroceries);
    }

    const handleEditClick = (event, grocery)=> {
        event.preventDefault();
        setEditGroceryId(grocery.id);

        const formValues = {
            store: grocery.store,
            item: grocery.item,
            price: grocery.price,
            date: grocery.date,
        }

        setEditFormData(formValues);
    }

    const handleCancelClick = () => {
        setEditGroceryId(null);
    }

    const submitData = () => {
        const data = groceries.map(g => {
            return {
                name: g.item,
                tags: [],
                price: g.price,
                shop: g.store,
                dateOfPurchase: g.date + "T00:00:00.000Z"
            }
        });

        console.log("Submiting: ", data);

        authFetch("/api/product/submit", { 
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }, 
            body: JSON.stringify(data)
        })
            .then(setGroceries([]));
    }

    useEffect(() => {
        authFetch("/api/shop")
            .then(response => response.json())
            .then(setShops);
    }, []);

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="bg-secondary rounded h-100 p-4">
                <Form onSubmit={handleAddFormSubmit}>
                    <label >Store Name</label>
                    <select
                        required
                        className="form-select"
                        name="store"
                        onChange={handleAddFormChange}
                    >
                        <option></option>
                        {shops.map(e => <option key={e.id}>{e.name}</option>)}
                    </select>
                    <label style={{marginTop:"8px"}}>Item Name</label>
                    <Input
                        required
                        type="text"
                        name="item"
                        onChange={handleAddFormChange}
                    />
                    <label style={{marginTop:"8px"}}>Item Price</label>
                    <Input
                        required
                        type="number"
                        step="0.01"
                        name="price"
                        onChange={handleAddFormChange}
                    />
                    <label style={{marginTop:"8px"}}>Purchase Date</label>
                    <Input
                        required
                        type="date"
                        name="date"
                        onChange={handleAddFormChange}
                    />
                    <Button color="primary" style={{marginTop:"11px"}}>Add</Button>
                </Form>

                <Form onSubmit = {handleEditClickSubmit}>
                    <Table>
                        <thead>
                            <tr>
                                <th>Store</th>
                                <th>Item</th>
                                <th>Price (€)</th>
                                <th>Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            {groceries.map((data) => (
                                <Fragment>
                                    {editGroceryId === data.id ? (<EditableRow handleCancelClick={handleCancelClick} 
                                    editFormData={editFormData} shops={shops} handleEditFormChange={handleEditFormChange} />
                                    ) : (
                                    <ReadOnlyRow data={data} handleEditClick={handleEditClick} handleDeleteClick={handleDeleteGrocery}/>)}
                                </Fragment>    
                            ))}
                        </tbody>
                    </Table>
                </Form>
                { groceries.length != 0 && <Button color="primary" className="m-2" onClick={submitData}>Submit</Button> }
            </div>
        </div>
    );
}
