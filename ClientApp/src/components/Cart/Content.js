import { useSelector } from "react-redux";
import { asyncStatus } from "../../state/AsyncStatus";
import { cartSelector, cartStatusSelector, cartViewSelector, cartStrategiesSelector } from "../../state/slices/CartSlice";
import { Spinner } from "../Spinner";
import { CartItem } from "./CartItem";
import { Strategy } from "./Strategy";

export const Content = () => {
    const products = useSelector(cartSelector);
    const cartStatus = useSelector(cartStatusSelector);
    const view = useSelector(cartViewSelector);
    const strategies = useSelector(cartStrategiesSelector);

    if (cartStatus == asyncStatus.LOADING) {
        return (
            <Spinner/>
        );
    }

    if (view === 'strategies') {
        if (strategies.length === 0) {
            return (
                <div className="position-relative w-100 h-75">
                    <p className="position-absolute start-50 top-50 translate-middle">There are no strategies. Submit the cart first.</p>
                </div>
            );
        }

        return (
            <div className="container-fluid pt-4 px-4 position-relative">
                <div className="row g-4">
                    {strategies.map((s, i) => <Strategy strategy={s} key={i}/>)}
                </div>
            </div>
        );
    }

    if (products.length === 0) {
        return (
            <div className="position-relative w-100 h-75">
                <p className="position-absolute start-50 top-50 translate-middle">There are no items in the cart</p>
            </div>
        );
    }

    return (
        <div className="container-fluid pt-4 px-4 position-relative">
            <div className="row g-4">
                {products.map(p => <CartItem product={p} key={p.id}/>)}
            </div>
        </div>
    );
}