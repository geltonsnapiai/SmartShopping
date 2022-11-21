import { useState } from "react";
import { Tag } from "../Tag/Tag";
import './ProductListItem.css';
import { useDispatch } from "react-redux";
import { animate } from "../../state/slices/CartHeaderIconSlice";
import { addItem } from "../../state/slices/CartSlice";

export const ProductListItem = (props) => {
    const [expanded, setExpanded] = useState(false);
    const dispatch = useDispatch();

    return (
        <div className ="col-sm-6 col-xl-3">
            <div className="bg-secondary rounded h-100 p-4 cartable">
                <div className="d-flex align-items-center justify-content-between">
                    <h6 className="mb-4">{props.product.name}</h6>
                    <span style={{fontSize: "24px"}} onClick={() => setExpanded(!expanded)}>
                        { expanded ? <i className="fa fa-angle-up cartable-arrow-icon"/> : <i className="fa fa-angle-down cartable-arrow-icon"/> }
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
                                    <td>{p.price.toFixed(2)} €</td>
                                    <td>{(new Date(p.date)).toLocaleDateString()}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                }
                <i className="fa fa-cart-plus cartable-cart-icon" onClick={() => {
                        dispatch(animate());
                        dispatch(addItem({
                            id: props.product.id,
                            name: props.product.name,
                            amount: 1,
                        }));
                    }}/>
            </div>
        </div>
    );
}