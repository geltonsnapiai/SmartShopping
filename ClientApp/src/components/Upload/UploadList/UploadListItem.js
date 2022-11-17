import { useState } from 'react';
import { Input, Form, Button } from "reactstrap";
import { deleteItem, updateItem } from '../../../state/slices/UploadListSlice';
import './UploadListItem.css';
import { useDispatch } from 'react-redux';

export const UploadListItem = (props) => {
    const [editing, setEditing] = useState(false);
    const dispatch = useDispatch();
    const shops = props.shops;

    const submitHandler = e => {
        let formData = {
            id: props.item.id,
            name: e.target.name.value,
            tags: [],
            price: e.target.price.value,
            shop: e.target.shop.value,
            dateOfPurchase: e.target.dateOfPurchase.value
        };
        dispatch(updateItem(formData));
        setEditing(false);
    };

    const deleteHandler = () => {
        dispatch(deleteItem(props.item.id));
        setEditing(false);
    };

    const cancelHandler = () => {
        setEditing(false);
    };

    return (
        <div className ="col-sm-12 col-md-6 col-xl-4">
            {editing ? (
                <div className="bg-secondary rounded p-4">
                    <Form onSubmit={submitHandler}>
                        <label >Store Name</label>
                        <select required className="form-select" name="shop" defaultValue={props.item.shop}>
                            <option></option>
                            {shops.map(e => <option key={e.id} selected={props.item.shop===e.name}>{e.name}</option>)}
                        </select>
                        <label style={{marginTop:"8px"}}>Item Name</label>
                        <Input required type="text" name="name" defaultValue={props.item.name}/>
                        <label style={{marginTop:"8px"}}>Item Price</label>
                        <Input required type="number" step="0.01" name="price" defaultValue={props.item.price}/>
                        <label style={{marginTop:"8px"}}>Purchase Date</label>
                        <Input required type="date" name="dateOfPurchase" defaultValue={props.item.dateOfPurchase}/>
                        <Button color="primary" outline className="mt-4 ms-2" onClick={deleteHandler}>Delete</Button>
                        <Button color="primary" outline className="mt-4 ms-2" onClick={cancelHandler}>Cancel</Button>
                        <Button type="submit" color="primary" className="mt-4 ms-2">Save</Button>
                    </Form>
                </div>
            ) : (
                <div className="bg-secondary rounded d-flex align-items-center justify-content-between p-4 hoverable" onClick={() => setEditing(true)}>
                    <div>
                        <p className="mb-2" style={{fontSize: "24px", color: "var(--primary)"}}>{props.item.name}</p>
                        <h6 className="mb-2">{props.item.price} €</h6>
                    </div>
                    <div>
                        <div className="text-end">
                            <p className="mb-2 mt-2">{props.item.shop}</p>
                            <p>{props.item.dateOfPurchase}</p>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}