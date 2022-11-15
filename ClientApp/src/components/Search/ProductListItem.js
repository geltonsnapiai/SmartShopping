import { useState } from "react";
import { Tag } from "./Tag";

export const ProductListItem = (props) => {
    const [expanded, setExpanded] = useState(false);

    console.log("product: ", props.product);

    return (
        <div className ="col-sm-6 col-xl-3">
            <div className="bg-secondary rounded h-100 p-4">
                <div className="d-flex align-items-center justify-content-between">
                    <h6 className="mb-4">{props.product.name}</h6>
                    <span style={{fontSize: "24px"}} onClick={() => setExpanded(!expanded)}>
                        { expanded ? <i className="fa fa-angle-up"/> : <i className="fa fa-angle-down"/> }
                    </span>
                </div>
                <div>
                    {props.product.prices.map((p, i) => <Tag key={i} type="shop" name={p.shop}/>)}
                    {props.product.tags.map((t, i) => <Tag key={i} type="tag" name={t}/>)}
                </div>
                {
                    expanded &&
                    <table className="table table-hover mt-4">
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
                }
            </div>
        </div>
    );
}