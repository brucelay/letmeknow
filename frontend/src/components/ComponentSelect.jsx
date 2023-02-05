import { createSignal } from "solid-js";

export default function ComponentSelect(props) {
  const addFunction = (func) => {
    console.log(func);
    const [options, setOptions] = createSignal({});
    props.setFunctions([...props.functions, { ...func}]);
  };

  return (
    <>
      <For  each={props.modules()}>
        {(module) => (
            <div class="m-3">
                <button className="capitalize bg-gray-800 hover:bg-orange-600 text-white text-xl font-bold py-2 px-4 rounded w-6/12"
                        onClick={() => addFunction(module)}>{module.function}</button>
            </div>
        )}
      </For>
    </>
  );
}
