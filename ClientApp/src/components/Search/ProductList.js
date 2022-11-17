import { useSelector } from "react-redux"
import { Spinner } from "reactstrap";
import { asyncStatus } from "../../state/AsyncStatus";
import { productListStatusSelector, productListSelector } from "../../state/slices/ProductListSlice"
import { ProductListItem } from "./ProductListItem"

export const ProductList = () => {
    const products = useSelector(productListSelector);
    const status = useSelector(productListStatusSelector);

    if (status === asyncStatus.LOADING) {
        return (
            // TODO: Spinner / loading animation
            <></>
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