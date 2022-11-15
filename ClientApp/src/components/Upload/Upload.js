import React, { useEffect } from "react";
import { Input, Form, Button } from "reactstrap";
import { useState } from "react";
import { UploadList } from "./UploadList/UploadList";
import { addItem, deleteAll } from "./UploadList/UploadListSlice";
import { authFetch } from "../../auth/AuthFetch";
import { useDispatch, useSelector } from "react-redux";
import { nanoid } from "nanoid";

export function Upload() {
    const uploadList = useSelector(store => store.uploadList);
    const [shops, setShops] = useState([]);
    const dispatch = useDispatch();

    useEffect(() => {
        authFetch("/api/shop")
            .then(response => response.json())
            .then(setShops);
    }, []);

    const submitHandler = (e) => {
        e.preventDefault();

        let formData = {
            id: nanoid(),
            name: e.target.name.value,
            tags: [],
            price: e.target.price.value,
            shop: e.target.shop.value,
            dateOfPurchase: e.target.dateOfPurchase.value
        };

        console.log("Adding data: ", formData);
        dispatch(addItem(formData));
        console.log("Added");
    };

    const uploadHandler = () => {
        authFetch("/api/product/submit", { 
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }, 
            body: JSON.stringify(uploadList),
        })
            .then(dispatch(deleteAll()));
    };

    const deleteHandler = () => {
        dispatch(deleteAll());
    };

    return (
        <div className="container-fluid pt-4 px-4">
            <div class="row g-4">
                <div class="col-sm-12 col-xl-6">
                    <div className="bg-secondary rounded p-4">
                        <Form onSubmit={submitHandler}>
                            <label >Store Name</label>
                            <select required className="form-select" name="shop" >
                                <option></option>
                                {shops.map(e => <option key={e.id}>{e.name}</option>)}
                            </select>
                            <label style={{marginTop:"8px"}}>Item Name</label>
                            <Input required type="text" name="name" />
                            <label style={{marginTop:"8px"}}>Item Price</label>
                            <Input required type="number" step="0.01" name="price" />
                            <label style={{marginTop:"8px"}}>Purchase Date</label>
                            <Input required type="date" name="dateOfPurchase"/>
                            <Button type="submit" color="primary" className="mt-4">Add</Button>
                        </Form>
                    </div>
                </div>
                <div class="col-sm-12 col-xl-6">
                    <div className="bg-secondary rounded h-100 p-4">
                        <h4>Here will be element for uploading photos of receipts</h4>
                    </div>
                </div>
                <UploadList shops={shops}/>
                {uploadList.length !== 0 && (
                    <div class="container-fluid pt-4 px-4">
                        <div class="bg-secondary text-center rounded p-4">
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <Button color="primary" onClick={uploadHandler}>Upload</Button>
                                <Button color="primary" onClick={deleteHandler}>Delete All</Button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}
