import { useSelector } from "react-redux";
import { cartSelector } from "../../state/slices/CartSlice";
import { CartItem } from "./CartItem";

export const Content = () => {
    const products = useSelector(cartSelector);
    
    return (
        <div className="container-fluid pt-4 px-4 position-relative">
            <div className="row g-4">
                {products.map(p => <CartItem product={p} />)}
            </div>
        </div>
    );
}