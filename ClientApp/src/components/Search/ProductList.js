import { ProductListItem } from "./ProductListItem"

function ProductList(props) {
    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                {props.products.map(p => <ProductListItem key={p.id} product={p}/>)}
            </div>
        </div>
    )
}

export default ProductList;