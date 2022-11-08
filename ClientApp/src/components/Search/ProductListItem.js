
export const ProductListItem = (props) => {
    return (
        <div class="col-sm-12 col-md-6 col-xl-4">
            <div className="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                <h5>{props.product.name}</h5>
                <p>{props.product.shops.join(", ")}</p>
            </div>
        </div>
    );
}