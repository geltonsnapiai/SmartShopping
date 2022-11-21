import { useDispatch, useSelector } from "react-redux";
import { Button } from "reactstrap";
import { cartSelector, deleteAll, generateStrategies, cartViewSelector, changeView } from "../../state/slices/CartSlice";
import { store } from "../../state/Store";

export const Controls = () => {
    const dispatch = useDispatch();
    const products = useSelector(cartSelector);
    const view = useSelector(cartViewSelector);

    const submitHandler = () => {
        console.log("Cart submited: ", products);
        store.dispatch(generateStrategies());
    };

    const viewButtonName = (view === 'strategies' ? 'Cart' : 'Strategies');
    const nextView = (view === 'strategies' ? 'cart' : 'strategies');

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                <div className ="col-sm-12 col-md-6 col-xl-4 mx-auto">
                    <div className="bg-secondary rounded h-100 p-4">
                        <div className="d-flex align-items-center justify-content-between">
                            <Button color="primary" disabled={products.length === 0 || view === 'strategies'} onClick={() => {dispatch(deleteAll())}}>Empty cart</Button>
                            <Button color="primary" outline onClick={() => dispatch(changeView(nextView))}>{viewButtonName}</Button>
                            <Button color="primary" disabled={products.length === 0 || view === 'strategies'} onClick={submitHandler}>Submit</Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}