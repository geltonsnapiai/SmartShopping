import { useSelector } from "react-redux"
import { asyncStatus } from "../../state/AsyncStatus";
import { productListStatusSelector, productListSelector } from "../../state/slices/ProductListSlice"
import { Spinner } from "../Spinner";
import { ProductListItem } from "./ProductListItem"

export const ProductList = () => {
    const products = useSelector(productListSelector);
    const status = useSelector(productListStatusSelector);

    if (status === asyncStatus.LOADING) {
        return (
            <Spinner/>
        );
    }

    return (
        <div className="container-fluid pt-4 px-4 position-relative">
            <div className="row g-4">
                {products.map(p => <ProductListItem key={p.id} product={p}/>)}
            </div>
        </div>
    )
}