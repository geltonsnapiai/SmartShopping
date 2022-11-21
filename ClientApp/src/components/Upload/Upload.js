import React, { useEffect } from "react";
import { Input, Form, Button } from "reactstrap";
import { UploadList } from "./UploadList/UploadList";
import { addItem, deleteAll, uploadProducts, uploadProductsSelector, uploadProductsStatusSelector } from "../../state/slices/UploadProductsSlice";
import { shopsSelector, shopsStatusSelector, loadShops } from "../../state/slices/ShopsSlice";
import { useDispatch, useSelector } from "react-redux";
import { nanoid } from "nanoid";
import { store } from "../../state/Store";
import { asyncStatus } from "../../state/AsyncStatus";

export function Upload() {
    const products = useSelector(uploadProductsSelector);
    const productsStatus = useSelector(uploadProductsStatusSelector);
    const shops = useSelector(shopsSelector);
    const shopsStatus = useSelector(shopsStatusSelector);
    const dispatch = useDispatch();

    useEffect(() => {
        if (shopsStatus === asyncStatus.IDLE) {
            store.dispatch(loadShops());
        }
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

        dispatch(addItem(formData));
    };

    const uploadHandler = () => {
        store.dispatch(uploadProducts());
    };

    const deleteHandler = () => {
        dispatch(deleteAll());
    };

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                <div className="col-sm-12 col-xl-6">
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
                <div className="col-sm-12 col-xl-6">
                    <div className="bg-secondary rounded h-100 p-4">
                        <h4>Here will be element for uploading photos of receipts</h4>
                    </div>
                </div>
                <UploadList shops={shops}/>
                {products.length !== 0 && (
                    <div className="container-fluid pt-4 px-4">
                        <div className="bg-secondary text-center rounded p-4">
                            <div className="d-flex align-items-center justify-content-between mb-4">
                                <Button color="primary" onClick={uploadHandler} disabled={productsStatus === asyncStatus.LOADING}>Upload</Button>
                                <Button color="primary" onClick={deleteHandler} disabled={productsStatus === asyncStatus.LOADING}>Delete All</Button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}
