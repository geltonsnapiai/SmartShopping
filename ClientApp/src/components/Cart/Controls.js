import { useDispatch, useSelector } from "react-redux";
import { Button } from "reactstrap";
import { cartSelector, deleteAll } from "../../state/slices/CartSlice";

export const Controls = () => {
    const dispatch = useDispatch();
    const products = useSelector(cartSelector);

    const submitHandler = () => {
        console.log("Cart submited: ", products);
    };

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                <div className ="col-sm-6 col-xl-3">
                    <div className="bg-secondary rounded h-100 p-4">
                        <div className="d-flex align-items-center justify-content-between">
                            <Button color="primary" disabled={products.length === 0} onClick={() => {dispatch(deleteAll())}}>Empty cart</Button>
                            <Button color="primary" disabled={products.length === 0} onClick={submitHandler}>Submit=</Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}