namespace Breifico.Functional

module LinkedList =
    type 'a LinkedListNode = FilledNode of 'a * LinkedListNode<'a> | NilNode

    let EmptyList = LinkedListNode.NilNode

    let rec add item head = function
        | FilledNode(a, node)-> add item head node
        | NilNode -> FilledNode(item, NilNode)