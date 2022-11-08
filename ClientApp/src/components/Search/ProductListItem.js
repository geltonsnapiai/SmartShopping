import { useState } from "react";

export const ProductListItem = (props) => {
    const [expanded, setExpanded] = useState(false);

    return (
        <div className  ="col-sm-12 col-md-6 col-xl-4">
            <div className="bg-secondary rounded h-100 p-4">
                <div className="d-flex align-items-center justify-content-between">
                    <h6 className="mb-4">{props.product.name}</h6>
                    <span style={{fontSize: "24px"}} onClick={() => setExpanded(!expanded)}>
                        { expanded ? <i className="fa fa-angle-up"/> : <i className="fa fa-angle-down"/> }
                    </span>
                </div>
                {
                    expanded ? 
                    <table className="table table-hover">
                        <tbody>
                            {props.product.prices.map(p => (
                                <tr>
                                    <td>{p.shop}</td>
                                    <td>{p.price} €</td>
                                    <td>{(new Date(p.date)).toLocaleDateString()}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    : 
                    <p>{props.product.prices.map(p => p.shop).join(", ")}</p>
                }
            </div>
        </div>
    );
}