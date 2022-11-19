import { useDispatch } from "react-redux";
import { deleteItem, updateItem } from "../../state/slices/CartSlice";
import "./CartItem.css";

export const CartItem = (props) => {
    const dispatch = useDispatch();
    return (
        <div className ="col-sm-6 col-xl-3">
            <div className="bg-secondary rounded h-100 p-4">
                <div className="d-flex align-items-center justify-content-between">
                    <h5 className="">{props.product.name}</h5>
                    <div className="car-item-amount fs-5 fs-b m-0">
                        <i className="fa fa-plus me-2" onClick={() => {
                            let newAmount = props.product.amount + 1;
                            let { id, name, _ } = props.product;
                            dispatch(updateItem({id, name, amount: newAmount}));
                        }}/>
                        <span>{props.product.amount}</span>
                        <i className="fa fa-minus ms-2" onClick={() => {
                            let newAmount = props.product.amount - 1;
                            if (newAmount === 0)
                                dispatch(deleteItem(props.product.id));
                            else {
                                let { id, name, _ } = props.product;
                                dispatch(updateItem({id, name, amount: newAmount}));
                            }
                        }}/>
                    </div>
                </div>
            </div>
        </div>
    );
}